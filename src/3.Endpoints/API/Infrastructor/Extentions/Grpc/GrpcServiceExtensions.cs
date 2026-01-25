using Master.Data.Core.Resources;
using Master.Data.Endpoints.API.Features.ServiceFeatures.Grpc;
using Master.Data.Endpoints.API.Infrastructor.Extentions.Grpc.Interceptors;
using Master.Data.Endpoints.API.Infrastructor.Extentions.Grpc.Services.CallContextAccessor;

namespace Master.Data.Endpoints.API.Infrastructor.Extentions.Grpc;

public static class GrpcServiceExtensions
{
    public static IServiceCollection AddProjectGrpc(this IServiceCollection services)
    {
        services.AddTransient<IGrpcServerCallContextAccessor, GrpcServerCallContextAccessor>();
        services.AddScoped<GrpcContextMiddleware>();
        services.AddScoped<LoggingInterceptor>();
        services.AddScoped<ExceptionInterceptor>();
        //services.AddScoped<ApiKeyClientInterceptor>();
        //services.AddSingleton<ApiKeyServerInterceptor>();

        services.AddGrpc(options =>
        {
            options.Interceptors.Add<ExceptionInterceptor>();
            options.Interceptors.Add<GrpcContextMiddleware>();
            options.Interceptors.Add<LoggingInterceptor>();
            //options.Interceptors.Add<ApiKeyServerInterceptor>();
            options.MaxSendMessageSize = 10 * 1024 * 1024; // 10MB
        }).AddJsonTranscoding();

        services.AddCors(o => o.AddPolicy(ProjectConsts.GRPC_CORS_NAME, builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
        }));

        return services;
    }

    public static WebApplication MapProjectGrpcServices(this WebApplication application)
    {
        application.UseGrpcWeb();

        application.MapGrpcService<ServiceFeatureGrpcService>()
            .EnableGrpcWeb()
            .RequireCors(ProjectConsts.GRPC_CORS_NAME);

        return application;
    }

    public static IServiceCollection AddGrpcClients(this IServiceCollection services)
    {
        services.AddTransient<LoggingInterceptor>();
        services.AddTransient<ExceptionInterceptor>();
        //services.AddTransient<ApiKeyClientInterceptor>();

        //#region Factor
        //services.AddGrpcClient<FactorService.FactorServiceClient>((provider, options) =>
        //{
        //    var grpcOption = provider.GetRequiredService<GrpcOption>();
        //    var factorsGrpcServer = grpcOption.GrpcServers.FirstOrDefault(c => c.ServerName == ProjectConsts.FACTORS_GRPC_CLIENT_NAME);
        //    options.Address = new Uri(factorsGrpcServer?.ServerBaseAddress ?? string.Empty);
        //})
        //.ConfigurePrimaryHttpMessageHandler(() =>
        //{
        //    var handler = new HttpClientHandler();
        //    handler.ServerCertificateCustomValidationCallback =
        //        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        //    return handler;
        //})
        //.AddInterceptor<ExceptionInterceptor>()
        //.AddInterceptor<LoggingInterceptor>()
        //.AddInterceptor<ApiKeyClientInterceptor>()
        //.AddInterceptor(provider =>
        //     ApiKeyInterceptorFactory.Create(provider, ProjectConsts.FACTORS_GRPC_CLIENT_NAME));
        //#endregion

        //// پیکربندی Polly برای retry (اختیاری)
        //services.AddPolicyRegistry()
        //    .Add("grpcRetryPolicy", Policy.Handle<RpcException>(ex =>
        //            ex.StatusCode == StatusCode.Unavailable ||
        //            ex.StatusCode == StatusCode.DeadlineExceeded)
        //        .WaitAndRetryAsync(3, retryAttempt =>
        //            TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

        return services;
    }
}