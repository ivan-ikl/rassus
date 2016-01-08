using Andacol.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Andacol.Endpoint.Extensions
{
    public static class Filtering
    {
        public static IQueryable<AskedQuestion> WherePending(this IQueryable<AskedQuestion> questions, string userId) => questions.Where(a => !a.AnsweredBy.Any(u => u.Identifier == userId));

    }
}