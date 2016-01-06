using Andacol.Core.Interfaces;
using System;
using System.Linq;

namespace Andacol.Core.Extensions
{
    public static class Filtering
    {

        public static IQueryable<T> WhereActive<T>(this IQueryable<T> items) where T : IExpireable => items.Where(i => i.DateExpired < DateTime.UtcNow && i.DateStarted >= DateTime.UtcNow);

    }
}
