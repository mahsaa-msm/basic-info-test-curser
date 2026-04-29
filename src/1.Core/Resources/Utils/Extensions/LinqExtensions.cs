using System.Linq.Expressions;

namespace Vehicle.Insurance.Core.Resources.Utils.Extensions;

public static class LinqExtensions
{
    #region IEnumrable WhereIf
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source,
                                        bool condition,
                                        Func<T, bool> predicate)
    {
        if (!condition)
            return source;

        return source.Where(predicate);
    }

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source,
                                            bool condition,
                                            Func<T, int, bool> predicate)
    {
        if (!condition)
            return source;

        return source.Where(predicate);
    }

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source,
                                            bool condition,
                                            Func<T, bool> ifPredicate,
                                            Func<T, bool> elsePredicate)
    {
        return condition
            ? source.Where(ifPredicate)
            : source.Where(elsePredicate);
    }

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source,
                                            bool condition,
                                            Func<T, int, bool> ifPredicate,
                                            Func<T, int, bool> elsePredicate)
    {
        return condition
            ? source.Where(ifPredicate)
            : source.Where(elsePredicate);
    }
    #endregion

    #region IQueryable WhereIf
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query,
                                           bool condition,
                                           Expression<Func<T, bool>> ifPredicate,
                                           Expression<Func<T, bool>> elsePredicate)
    {
        return condition
            ? query.Where(ifPredicate)
            : query.Where(elsePredicate);
    }

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query,
                                       bool condition,
                                       Expression<Func<T, int, bool>> ifPredicate,
                                       Expression<Func<T, int, bool>> elsePredicate)
    {
        return condition
            ? query.Where(ifPredicate)
            : query.Where(elsePredicate);
    }
    #endregion
}

