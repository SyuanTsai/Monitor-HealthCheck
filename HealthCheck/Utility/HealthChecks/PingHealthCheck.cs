using System.Net.NetworkInformation;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheck.Utility.HealthChecks;

public class PingHealthCheck: IHealthCheck
{
    private readonly string _host;
    private readonly int    _timeout;
 
    /// <summary>
    ///     要測試的網址/IP
    /// </summary>
    /// <param name="host">IP或者是網址</param>
    /// <param name="timeout">警告時間</param>
    public PingHealthCheck(string host, int timeout)
    {
        _host    = host;
        _timeout = timeout;
    }
 
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            using var ping = new Ping();

            var reply = await ping.SendPingAsync(_host, _timeout);
            if (reply.Status != IPStatus.Success)
            {
                return HealthCheckResult.Unhealthy();
            }
 
            Console.WriteLine($"{_host},{reply.RoundtripTime}, {_timeout}");
            return reply.RoundtripTime >= _timeout ? HealthCheckResult.Degraded() : HealthCheckResult.Healthy();
        }
        catch
        {
            return HealthCheckResult.Unhealthy();
        }
    }
}