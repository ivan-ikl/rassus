using Andacol.Core.Models;
using Andacol.Core.Extensions;
using System.Web.Mvc;
using System.Data.Entity;
using Andacol.Endpoint.Extensions;
using System.Linq;
using Andacol.Endpoint.Models;
using System;

namespace Andacol.Endpoint.Controllers
{
    public class HomeController : Controller
    {
        private AndacolContext Db { get; } = new AndacolContext();

        public ActionResult Index() => View(Db.Questions.OfType<OptionalQuestion>().WhereActive().SelectViewModels().ToList().Union(Db.Questions.OfType<ScoreQuestion>().WhereActive().SelectViewModels()));

        public ActionResult History()
        {
            ViewBag.History = true;
            return View(nameof(Index), Db.Questions.OfType<OptionalQuestion>().WhereHistory().SelectViewModels().ToList().Union(Db.Questions.OfType<ScoreQuestion>().WhereHistory().SelectViewModels()));
        }

        public ActionResult Create() => View("Question");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionViewModel model, FormCollection form)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Min");
            ModelState.Remove("Max");
            if (!ModelState.IsValid)
            {
                return View("Question", model);
            }

            var dbModel = (model.QuestionType == QuestionType.OptionalQuestion) ? (Question)new OptionalQuestion
            {
                AnswerOptions = form.AllKeys.Where(k => k.StartsWith("option_")).Select(k => new AnswerOption { Text = form.Get(k) }).ToList(),
            } : new ScoreQuestion
            {
                Min = model.Min,
                Max = model.Max
            };

            dbModel.Schedule = model.Schedule;
            dbModel.QuestionText = model.QuestionText;

            Db.Questions.Add(dbModel);
            Db.SaveChanges();

            Db.AskQuestions();

            return RedirectToAction(nameof(Details), new { Id = dbModel.Id });
        }

        public ActionResult Details(long Id) => View("Question", Db.Questions.OfType<OptionalQuestion>().SelectViewModels().FirstOrDefault(q => q.Id == Id) ?? Db.Questions.OfType<ScoreQuestion>().SelectViewModels().FirstOrDefault(q => q.Id == Id));

        /// <summary>
        /// Ends the poll
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(QuestionViewModel model)
        {
            var dbModel = Db.Questions.FirstOrDefault(q => q.Id == model.Id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }
            dbModel.DateExpired = DateTime.UtcNow;
            Db.SaveChanges();
            return RedirectToAction(nameof(Details), new { Id = model.Id });
        }

    }
}