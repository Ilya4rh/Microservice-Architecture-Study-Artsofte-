﻿using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog;
using Serilog.Core;
using Serilog.Configuration;

namespace ExampleCore.Logs;

/// <summary>
/// Конфигурации Серилога
/// </summary>
public static class SerilogConfiguration
{
    /// <summary>
    /// Добавление логирования
    /// </summary>
    public static IServiceCollection AddLoggerServices(this IServiceCollection services)
    {
        return services
            .AddSingleton(Log.Logger);
    }

    /// <summary>
    /// Получить конфигурацию
    /// </summary>
    public static LoggerConfiguration GetConfiguration(this LoggerConfiguration loggerConfiguration)
    {
        var logFormat = "{Timestamp:HH:mm:ss:ms} LEVEL:[{Level}] TRACE:|{TraceId}| THREAD:|{ThreadId}|{TenantId} {Message}{NewLine}{Exception}";

        return loggerConfiguration
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Information)
            .MinimumLevel.Is(LogEventLevel.Information)
            .Enrich.WithThreadId()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: logFormat);
    }
}