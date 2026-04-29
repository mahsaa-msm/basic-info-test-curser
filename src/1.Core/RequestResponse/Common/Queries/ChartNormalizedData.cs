namespace Vehicle.Insurance.Core.RequestResponse.Common.Queries;

public class ChartNormalizedData<TXValue, TYValue, TMetaData>
    where TXValue : notnull
    where TYValue : notnull
    where TMetaData : class, new()
{
    public List<ChartData<TXValue, TYValue>> ChartData { get; set; } = new();
    public TMetaData? MetaData { get; set; }
    public long Count { get; set; }
}

public class ChartNormalizedData<TXValue, TYValue>
    where TXValue : notnull
    where TYValue : notnull
{
    public List<ChartData<TXValue, TYValue>> ChartData { get; set; } = new();
    public long Count { get; set; }
}

public sealed class ChartData<TXValue, TYValue>
    where TXValue : notnull
    where TYValue : notnull
{
    public TXValue Label { get; set; } = default!;
    public TYValue Value { get; set; } = default!;
}

