using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.Common.Queries;

public abstract class ChartQuery<TXValue, TYValue, TMetaData> : IQuery<ChartNormalizedData<TXValue, TYValue, TMetaData>>
    where TXValue : notnull
    where TYValue : notnull
    where TMetaData : class, new()
{
    public int Skip { get; set; } = 0;
    public int Top { get; set; } = 50;
}

public abstract class ChartQuery<TXValue, TYValue> : IQuery<ChartNormalizedData<TXValue, TYValue>>
    where TXValue : notnull
    where TYValue : notnull
{
    public int Skip { get; set; } = 0;
    public int Top { get; set; } = 50;
}
