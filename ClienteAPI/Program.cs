using ClienteAPI.Data;
using Microsoft.EntityFrameworkCore;
using Prometheus;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;


var builder = WebApplication.CreateBuilder(args);

// 1) Configuración de DbContext
builder.Services.AddDbContext<BdClientesContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ClienteDB")));

// 2) HealthChecks básicos (con SQL Server)
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("ClienteDB"));

// 3) Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4) Métricas de Prometheus
app.UseMetricServer();
app.UseHttpMetrics();

// 5) Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// 6) Pipeline de la API
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 7) Endpoint de HealthChecks
app.MapHealthChecks("/health");

// 8) HealthChecks expuestos con métricas
//    Esto crea un contador de ejecuciones y un gauge de estado (0/1).
var hcRegistry = app.Services.GetRequiredService<HealthCheckService>();
var hcOptions = new HealthCheckOptions { Predicate = _ => true };
app.MapHealthChecks("/health-db", hcOptions);

// Ligamos los endpoints a métricas de Prometheus
Metrics.DefaultRegistry.AddBeforeCollectCallback(() =>
{
    var report = hcRegistry.CheckHealthAsync(hcOptions.Predicate).Result;
    // Gauge: 1 = healthy, 0 = unhealthy
    var status = report.Status == HealthStatus.Healthy ? 1 : 0;
    Metrics.CreateGauge("db_health_status", "Estado de la BD (1=up,0=down)").Set(status);
});


app.Run();
