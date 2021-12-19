namespace GrpcServiceSample
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Grpc.Core;
    using Grpc.Core.Interceptors;
    using Microsoft.Extensions.Logging;

    public sealed class MsgInterceptor : Interceptor
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetricInterceptor"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        public MsgInterceptor(ILogger<MsgInterceptor> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
            where TRequest : class
            where TResponse : class
        {

            // Console.WriteLine($"Interceptor {request} {DateTime.Now}");
            var response = await continuation(request, context);
            Console.WriteLine($"Interceptor : {response} {request}");
            return response;
        }
    }
}