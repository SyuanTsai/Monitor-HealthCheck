using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

EnvInfo();

CheckEnvSetting();

HealthCheckSetting(builder);

var app = builder.Build();

app.UseHealthChecks
(
    "/hc", new HealthCheckOptions
    {
        Predicate = _ => true, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
      , ResultStatusCodes =
        {
            [HealthStatus.Healthy]   = StatusCodes.Status200OK
          , [HealthStatus.Degraded]  = StatusCodes.Status503ServiceUnavailable
          , [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
        }
    }
);

app.UseHealthChecksUI
(
    opt => { opt.UIPath = "/Hc-Ui"; }
);

app.MapGet("/", () => "Hello World!");

app.Run();