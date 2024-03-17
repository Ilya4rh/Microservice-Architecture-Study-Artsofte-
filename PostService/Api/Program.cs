using Services;
using Infastractes;
using ExampleCore.HttpLogic;
using ProfileConnectionLib;
using ExampleCore.TraceIdLogic;
using ExampleCore.Logs;
using Serilog;
using Serilog.Sinks.SystemConsole;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.TryAddServices();
builder.Services.TryAddInfastractes();
builder.Services.TryAddHttpRequestService();
builder.Services.TryAddProfileConnectionServices();
builder.Services.TryAddTraceId();
builder.Services.AddLoggerServices();
builder.Host.UseSerilog((context, config) => config.GetConfiguration());

var app = builder.Build();
app.UseMiddleware<ReadTraceId>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
