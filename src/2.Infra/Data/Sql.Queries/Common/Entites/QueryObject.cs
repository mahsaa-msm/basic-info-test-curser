namespace Master.Data.Infra.Data.Sql.Queries.Common.Entites;

public abstract class QueryObject : QueryObject<long>
{
}

public abstract class QueryObject<T>
{
    public T Id { get; set; }
    public Guid BusinessId { get; set; }
    public string? CreatedByUserId { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public string? ModifiedByUserId { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
}