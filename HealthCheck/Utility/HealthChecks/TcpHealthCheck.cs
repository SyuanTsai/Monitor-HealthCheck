using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheck.Utility.HealthChecks;

public class TcpHealthCheck : IHealthCheck
{
    private readonly string _host;
    private readonly int    _port;
    private readonly int    _timeout;

    /// <summary>
    ///     要測試的網址/IP
    /// </summary>
    /// <param name="host">IP或者是網址</param>
    /// <param name="port">Port</param>
    /// <param name="timeout">警告時間</param>
    public TcpHealthCheck(string host, int port, int timeout)
    {
        _host    = host;
        _port    = port;
        _timeout = timeout;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context
                                                        , CancellationToken  cancellationToken = default)
    {
        try
        {
            using var ping = new TcpClient(_host, _port);
            ping.SendTimeout = _timeout;
            var result = ping.NoDelay;
            
            ping.Close();    
            return result ? HealthCheckResult.Degraded() : HealthCheckResult.Healthy();
        }
        catch
        {
            return HealthCheckResult.Unhealthy();
        }
    }
}