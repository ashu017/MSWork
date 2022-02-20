using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcServiceSample
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        // Unary Call
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name,
                Call = request.Phone
            });
        }

        //Client Streaming
        public override async Task<returnTickets> movies(IAsyncStreamReader<bookTicket> requestStream, ServerCallContext context)
        {
            int totalTickets = 0;
            while (await requestStream.MoveNext())
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    break;
                }
                totalTickets++;
            }
            return (new returnTickets
            {
                TotalTickets = totalTickets,
            });
        }

        // Server Streaming
        public override async Task GetLocations(GetLocationsRequest request, IServerStreamWriter<TripResponse> responseStream, ServerCallContext context)
        {
            var remainingDistance = request.TotalDistance;
            var speed = 20;
            while(remainingDistance > 0.0 && !context.CancellationToken.IsCancellationRequested)
            {
                remainingDistance -= speed * 2;
                await responseStream.WriteAsync(new TripResponse()
                {
                    RemainingDistance = remainingDistance,
                    TimeToDestination = remainingDistance/speed,
                    CurrentSpeedms = speed,
                    CurrentSpeedkh = speed * (3.6)
                });
                Thread.Sleep(2000);
            }
        }

        // Bi Directional Streaming
        public override async Task navigate(IAsyncStreamReader<TripRequest> requestStream, IServerStreamWriter<TripResponse> responseStream, ServerCallContext context)
        {
            var remainingDistance = 10000;
            while (await requestStream.MoveNext())
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    break;
                }
                var tripRequest = requestStream.Current;
                remainingDistance -= tripRequest.DistanceTravelled;
                if (remainingDistance <= 0)
                {
                    break;
                }
                var elapsedDuration = tripRequest.ElapsedDuration;
                double currentSpeed = (tripRequest.DistanceTravelled * 1.0d) / elapsedDuration;
                int timeToReach = (int)(remainingDistance / currentSpeed);
                await responseStream.WriteAsync(new TripResponse()
                {
                    RemainingDistance = remainingDistance,
                    TimeToDestination = timeToReach,
                    CurrentSpeedms = currentSpeed,
                    CurrentSpeedkh = currentSpeed * (3.6)
                });
            }
        }
    }
}

