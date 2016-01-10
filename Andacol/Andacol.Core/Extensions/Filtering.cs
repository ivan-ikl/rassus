using Andacol.Core.Interfaces;
using Andacol.Core.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace Andacol.Core.Extensions
{
    public static class Filtering
    {

        public static IQueryable<T> WhereActive<T>(this IQueryable<T> items) where T : IExpireable => items.Where(i => i.DateExpired > DateTime.UtcNow && i.DateStarted <= DateTime.UtcNow);

        public static IQueryable<T> WhereHistory<T>(this IQueryable<T> items) where T : IExpireable => items.Where(i => i.DateExpired <= DateTime.UtcNow);

        public static IQueryable<Question> WhereDue(this IQueryable<Question> items) => items.WhereActive().Where(q => q.NextDue <= DateTime.UtcNow && q.AskedQuestions.Max(a => a.DateAsked) != q.NextDue);   

        public static IQueryable<AskedQuestion> WhereActive(this IQueryable<AskedQuestion> items) => items.Where(i => i.Question.DateExpired > DateTime.UtcNow && i.Question.DateStarted <= DateTime.UtcNow);

    }
}
