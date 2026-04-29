namespace Vehicle.Insurance.Core.Contracts.Common.Options;

public sealed class GrpcOption
{
    public string ApiKeyName { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
    public List<GrpcServer> GrpcServers { get; set; } = new();
}

public sealed class GrpcServer
{
    public string ServerName { get; set; } = default!;
    public string ServerBaseAddress { get; set; } = default!;
    public bool NeedAuth { get; set; }
    public string ApiKeyName { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
}

