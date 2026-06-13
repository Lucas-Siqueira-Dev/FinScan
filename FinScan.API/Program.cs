using FinScan.API.Data;
using Microsoft.EntityFrameworkCore;
using FinScan.API.Services;
using FinScan.API.Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddHttpClient<IIndicadoresFinanceirosService, BacenService>();
builder.Services.AddScoped<ISimulacaoInvestimentoService, SimulacaoInvestimentoService>();
builder.Services.AddScoped<ICategorizadorService, CategorizadorService>();
builder.Services.AddScoped<IOcrService, OcrService>();
builder.Services.AddScoped<IRendimentoStrategy, CdiRendimentoStrategy>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();