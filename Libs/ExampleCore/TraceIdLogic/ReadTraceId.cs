using ExampleCore.TraceLogic.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ExampleCore.TraceIdLogic;

public class ReadTraceId(RequestDelegate requestDelegate)
{
    private readonly RequestDelegate requestDelegate = requestDelegate;

    public async Task InvokeAsync(HttpContext context, IEnumerable<ITraceReader> traceReaderList)
    {
        foreach (var traceReader in traceReaderList)
            traceReader.WriteValue(context.Request.Headers["TraceId"]);

        await requestDelegate(context);
    }
}
