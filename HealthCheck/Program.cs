var builder = WebApplication.CreateBuilder(args);

EnvInfo();

CheckEnvSetting();

HealthCheckSetting(builder);

var app = builder.Build();

app.UseHealthChecksUI
(
    opt => { opt.UIPath = "/Hc-Ui"; }
);

app.MapGet("/", () => "Hello World!");

app.Run();