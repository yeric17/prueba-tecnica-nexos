using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Endpoints
{
    public static class HealthCheckHandler
    {
        public static RouteGroupBuilder MapHealthChecks(this  RouteGroupBuilder builder)
        {
            builder.MapGet("health", () => Results.Ok(new { status = "Healthy" }))
                .WithName("HealthCheck")
                .Produces(StatusCodes.Status200OK);

            return builder;
        }
    }
}
