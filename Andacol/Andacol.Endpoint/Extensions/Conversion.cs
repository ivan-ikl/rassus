using Andacol.Core.Models;
using Andacol.Endpoint.Models;
using System.Collections.Generic;
using System.Linq;

namespace Andacol.Endpoint.Extensions
{
    public static class Conversion
    {
        public static IQueryable<AskedQuestionViewModel> SelectViewModels(this IQueryable<AskedQuestion> questions)
        {
            var optional = questions.Select(q => q.Question).OfType<OptionalQuestion>().Join(questions, opt => opt.Id, q => q.Question.Id, (opt, q) => new { Asked = q, Question = opt }).Select(q => new
            {
                Id = q.Asked.Id,
                DateAsked = q.Asked.DateAsked,
                Options = q.Question.AnswerOptions,
                Min = 0,
                Max = 0,
                QuestionText  = q.Question.QuestionText,
                QuestionType = QuestionType.OptionalQuestion
            }).Distinct();
            var score = questions.Select(q => q.Question).OfType<ScoreQuestion>().Join(questions, opt => opt.Id, q => q.Question.Id, (opt, q) => new { Asked = q, Question = opt }).Select(q => new
            {
                Id = q.Asked.Id,
                DateAsked = q.Asked.DateAsked,
                Options = null as List<AnswerOption>,
                Min = q.Question.Min,
                Max = q.Question.Max,
                QuestionText = q.Question.QuestionText,
                QuestionType = QuestionType.ScoreQuestion
            }).Distinct();
            return optional.Concat(score).Select(m => new AskedQuestionViewModel {
                Id = m.Id,
                DateAsked = m.DateAsked,
                Options = m.Options,
                Min = m.Min,
                Max = m.Max,
                QuestionText = m.QuestionText,
                QuestionType = m.QuestionType
            });
        }

        public static IQueryable<QuestionViewModel> SelectViewModels(this IQueryable<OptionalQuestion> questions) => questions.Select(q => new QuestionViewModel
        {
            Id = q.Id,
            QuestionType = QuestionType.OptionalQuestion,
            ScheduleTicks = q.ScheduleTicks,
            DateStarted = q.DateStarted,
            DateExpired = q.DateExpired,
            QuestionText = q.QuestionText,
            Options = q.AnswerOptions.Select(a => new Option
            {
                Id = a.Id,
                Text = a.Text,
                Votes = a.OptionAnswers.Count()
            })
        });

        public static IQueryable<QuestionViewModel> SelectViewModels(this IQueryable<ScoreQuestion> questions) => questions.Select(q => new QuestionViewModel
        {
            Id = q.Id,
            QuestionType = QuestionType.ScoreQuestion,
            ScheduleTicks = q.ScheduleTicks,
            DateStarted = q.DateStarted,
            DateExpired = q.DateExpired,
            QuestionText = q.QuestionText,
            Max = q.Max,
            Min = q.Min,
            Scores = q.AskedQuestions.SelectMany(a => a.Answers.OfType<ScoreAnswer>().GroupBy(qa => qa.Score)).Select(g => new Score
            {
                Option = g.Key,
                Count = g.Count()
            })
        });

    }
}