# License scope

The MIT text in [LICENSES/MIT.txt](LICENSES/MIT.txt) is effective only for
these exact repository-authored paths:

- `README.md`
- `HealthCheck.sln`
- `HealthCheck/HealthCheck.csproj`
- `HealthCheck/Const/EnvKey.cs`
- `HealthCheck/Program.HealthCheck.cs`
- `HealthCheck/Program.cs`
- `HealthCheck/Utility/HealthChecks/PingHealthCheck.cs`
- `HealthCheck/Utility/HealthChecks/TcpHealthCheck.cs`

The runtime configuration files under `HealthCheck/appsettings*.json` and
`HealthCheck/Properties/launchSettings.json`, `.gitignore`, external NuGet
packages, ASP.NET Core, the .NET runtime, external services, generated output,
and unlisted files are excluded. No test project was present in the reviewed
tree; future test files require a separate review.
