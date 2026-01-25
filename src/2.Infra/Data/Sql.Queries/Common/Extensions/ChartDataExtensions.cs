using Master.Data.Core.RequestResponse.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Master.Data.Infra.Data.Sql.Queries.Common.Extensions;

public static class ChartDataExtensions
{
    /// <summary>
    /// Convert IQueryable to ChartNormalizedData query result that has a ChartData filed of type List<ChartData<TXValue,TYValue>>. 
    /// This useable for create x,y pair data for seeding chart data
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TXValue"></typeparam>
    /// <typeparam name="TYValue"></typeparam>
    /// <param name="entities"></param>
    /// <param name="xSelector"></param>
    /// <param name="ySelector"></param>
    /// <returns>ChartNormalizedData<TXValue, TYValue></returns>
    public static async Task<ChartNormalizedData<TXValue, TYValue>> ToChartNormalizedData<TEntity, TXValue, TYValue>(this IQueryable<TEntity> entities,
                                                                                                                     ChartQuery<TXValue, TYValue> query,
                                                                                                                     Func<TEntity, TXValue> xSelector,
                                                                                                                     Func<TEntity, TYValue> ySelector)
            where TXValue : notnull
            where TYValue : notnull
    {

        var chartData = await entities.ApplyChartFilter(query)
            .Select(entity => new ChartData<TXValue, TYValue>
            {
                Label = xSelector(entity),
                Value = ySelector(entity)
            }).ToListAsync();

        return new ChartNormalizedData<TXValue, TYValue>
        {
            ChartData = chartData,
            Count = chartData.Count,
        };
    }

    /// <summary>
    /// Convert IQueryable to ChartNormalizedData query result that has a ChartData filed of type List<ChartData<TXValue,TYValue>. 
    /// This useable for create x,y pair data for seeding chart data. 
    /// Also this has another field (MetaData) for return additional info with x,y pair data. 
    /// Note that the third parameter of this method receives a func for setting metaData and sets this func with the FirstOrDefaultAsync method available in EF in the metaData field of TMetaData. 
    /// Use Where for your metadata if necessary
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TXValue"></typeparam>
    /// <typeparam name="TYValue"></typeparam>
    /// <typeparam name="TMetaData"></typeparam>
    /// <param name="entities"></param>
    /// <param name="xSelector"></param>
    /// <param name="ySelector"></param>
    /// <param name="metaDataSelector"></param>
    /// <returns>ChartNormalizedData<TXValue, TYValue, TMetaData></returns>
    public static async Task<ChartNormalizedData<TXValue, TYValue, TMetaData>> ToChartNormalizedData<TEntity, TXValue, TYValue, TMetaData>(this IQueryable<TEntity> entities,
                                                                                                                                           ChartQuery<TXValue, TYValue, TMetaData> query,
                                                                                                                                           Func<TEntity, TXValue> xSelector,
                                                                                                                                           Func<TEntity, TYValue> ySelector,
                                                                                                                                           Func<TEntity, TMetaData>? metaDataSelector = null)
        where TXValue : notnull
        where TYValue : notnull
        where TMetaData : class, new()
    {
        var chartDataQuery = entities.ApplyChartFilter(query).Select(item => new
        {
            X = xSelector(item),
            Y = ySelector(item),
            MetaData = metaDataSelector != null ? metaDataSelector(item) : default
        });

        var chartData = new ChartNormalizedData<TXValue, TYValue, TMetaData>
        {
            ChartData = await chartDataQuery.Select(item =>
            new ChartData<TXValue, TYValue>
            {
                Label = item.X,
                Value = item.Y
            }).ToListAsync(),
            Count = chartDataQuery.Count(),
            MetaData = metaDataSelector != null ? await chartDataQuery.Select(item => item.MetaData).FirstOrDefaultAsync() : default(TMetaData),
        };

        return chartData;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TXValue"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="groupedDataQuery"></param>
    /// <param name="query"></param>
    /// <param name="resultSelector"></param>
    /// <returns></returns>
    public static async Task<ChartNormalizedData<TXValue, TResult>> ToChartNormalizedData<TEntity, TXValue, TResult>(this IQueryable<IGrouping<TXValue, TEntity>> groupedDataQuery,
                                                                                                                     Func<IEnumerable<TEntity>, TResult> resultSelector)
    where TXValue : notnull
    where TResult : notnull
    {
        var chartData = new ChartNormalizedData<TXValue, TResult>
        {
            ChartData = await groupedDataQuery
            .Select(item => new ChartData<TXValue, TResult>
            {
                Label = item.Key,
                Value = resultSelector(item.AsEnumerable())
            }).ToListAsync()
        };

        return chartData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TXValue"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TMetaData"></typeparam>
    /// <param name="groupedDataQuery"></param>
    /// <param name="query"></param>
    /// <param name="resultSelector"></param>
    /// <param name="metaDataSelector"></param>
    /// <returns></returns>
    public static async Task<ChartNormalizedData<TXValue, TResult, TMetaData>> ToChartNormalizedData<TEntity, TXValue, TResult, TMetaData>(this IQueryable<IGrouping<TXValue, TEntity>> groupedDataQuery,
                                                                                                                                           Func<IEnumerable<TEntity>, TResult> resultSelector,
                                                                                                                                           Func<TEntity, TMetaData>? metaDataSelector = null)
        where TXValue : notnull
        where TResult : notnull
        where TMetaData : class, new()
    {
        var chartDataQuery = groupedDataQuery.Select(item => new
        {
            X = item.Key,
            Y = resultSelector(item.AsEnumerable()),
            MetaData = metaDataSelector != null ? metaDataSelector(item.FirstOrDefault()) : default
        });

        var chartData = new ChartNormalizedData<TXValue, TResult, TMetaData>
        {
            ChartData = await chartDataQuery.Select(item =>
            new ChartData<TXValue, TResult>
            {
                Label = item.X,
                Value = item.Y
            }).ToListAsync(),
            Count = chartDataQuery.Count(),
            MetaData = metaDataSelector != null ? await chartDataQuery.Select(item => item.MetaData).FirstOrDefaultAsync() : default,
        };

        return chartData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TXValue"></typeparam>
    /// <typeparam name="TYValue"></typeparam>
    /// <param name="entities"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> ApplyChartFilter<TEntity, TXValue, TYValue>(this IQueryable<TEntity> entities,
                                                                                  ChartQuery<TXValue, TYValue> query)
        where TXValue : notnull
        where TYValue : notnull
        => entities.Skip(query.Skip).Take(query.Top);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TXValue"></typeparam>
    /// <typeparam name="TYValue"></typeparam>
    /// <typeparam name="TMetaData"></typeparam>
    /// <param name="entities"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> ApplyChartFilter<TEntity, TXValue, TYValue, TMetaData>(this IQueryable<TEntity> entities,
                                                                                             ChartQuery<TXValue, TYValue, TMetaData> query)
        where TXValue : notnull
        where TYValue : notnull
        where TMetaData : class, new()
        => entities.Skip(query.Skip).Take(query.Top);
}