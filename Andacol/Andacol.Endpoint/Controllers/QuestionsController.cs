using Andacol.Core.Extensions;
using Andacol.Core.Models;
using Andacol.Endpoint.Extensions;
using Andacol.Endpoint.Models;
using IKLSoftware.ODataHelpers.Attributes;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.OData;

namespace Andacol.Endpoint.Controllers
{

    [EntitySet(typeof(AskedQuestionViewModel))]
    public class QuestionsController : ODataController
    {

        private AndacolContext Db { get; } = new AndacolContext();

        [EnableQuery]
        public IQueryable<AskedQuestionViewModel> Get() => Db.AskedQuestions.WhereActive().ToList().AsQueryable().SelectViewModels();

        [EnableQuery]
        [ODataFunction]
        [ODataReturnsCollectionFromEntitySet(typeof(AskedQuestionViewModel))]
        [ODataParameter(typeof(string), "UserId")]
        public IQueryable<AskedQuestionViewModel> GetPending(string userId) => Db.AskedQuestions.WhereActive().WherePending(userId).ToList().AsQueryable().SelectViewModels();

        [HttpPost]
        [ODataAction]
        [ODataParameter(typeof(string), "UserId")]
        [ODataParameter(typeof(long), "Answer")]
        public IHttpActionResult SendAnswer([FromODataUri] long key, ODataActionParameters parameters)
        {
            var userId = (string)parameters["UserId"];
            var user = Db.Users.FirstOrDefault(u => u.Identifier == userId);
            if (user == null)
            {
                Db.Users.Add(user = new User { Identifier = userId });
                Db.SaveChanges();
            }

            var question = Db.AskedQuestions.WhereActive().WherePending(userId).Include(q => q.Question).FirstOrDefault(q => q.Id == key);
            if (user == null || question == null)
            {
                return NotFound();
            }
           
            question.AnsweredBy.Add(user);
            var answer = (long)parameters["Answer"];
            if (typeof(OptionalQuestion).IsAssignableFrom(question.Question.GetType()))
            {
                var option = ((OptionalQuestion)question.Question).AnswerOptions.FirstOrDefault(o => o.Id == answer);
                if (option == null)
                {
                    return NotFound();
                }
                question.Answers.Add(new OptionAnswer { AskedQuestion = question, AnswerOption = option });
            } else
            {
                if (answer < ((ScoreQuestion)question.Question).Min || answer > ((ScoreQuestion)question.Question).Max)
                {
                    return NotFound();
                }
                question.Answers.Add(new ScoreAnswer { AskedQuestion = question, Score = (int)answer });
            }
            Db.SaveChanges();

            return Ok(true);
        }

        [HttpGet]
        [ODataFunction]
        [ODataReturns(typeof(int))]
        public IHttpActionResult QuestionsAsked() => Ok(AndacolScheduler.TimesScheduled);

    }
}