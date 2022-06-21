// ReSharper disable once CheckNamespace

using System.Diagnostics;
using HealthCheck.Const;
using static System.Int32;

public partial class Program
{
    private static void HealthCheckSetting(WebApplicationBuilder webApplicationBuilder)
    {
        var strCycleSec = Environment.GetEnvironmentVariable("HealthCheckCycleSecond")!;
        var strTimeOut  = Environment.GetEnvironmentVariable("HealthCheckCycleSecond")!;
        TryParse(strCycleSec, out var cycleSec);
        TryParse(strTimeOut, out var timeOut);

        webApplicationBuilder.Services.AddHealthChecksUI
                             (
                                 setup =>
                                 {
                                     setup.SetEvaluationTimeInSeconds(cycleSec);
                                     setup.MaximumHistoryEntriesPerEndpoint(timeOut);
                                 }
                             )
                             .AddInMemoryStorage();
    }

    /// <summary>
    ///     檢查初始必要的參數
    /// </summary>
    /// <exception cref="NullReferenceException"></exception>
    private static void CheckEnvSetting()
    {
        var checkList = new List<string>
            {EnvKey.HealthCheckCycleSecond, EnvKey.HealthCheckTimeOutSecond};

        foreach (var check in checkList.Select(Environment.GetEnvironmentVariable)
                                       .Where(check => check is null))
        {
            throw new NullReferenceException($"Env參數：{check}未設定。");
        }
    }


    #region 環境相關設定

    private static void EnvInfo()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
        Dev(env);
        Release(env);
    }

    /// <summary>
    /// </summary>
    /// <param name="env"></param>
    [Conditional("DEBUG")]
    private static void Dev(string env)
    {
        Console.WriteLine("執行的組態設定 - Debug Mode");
        Console.WriteLine($"執行環境：{env}");
    }

    /// <summary>
    ///     指定使用IP模式，而不是Localhost。
    ///     讓本機開發中可以透過 Nginx 去使用DEV站台做測試。
    /// </summary>
    /// <param name="env"></param>
    [Conditional("RELEASE")]
    private static void Release(string env)
    {
        Console.WriteLine("執行的組態設定 - Release Mode");
        Console.WriteLine($"執行環境：{env}");
    }

    #endregion
}