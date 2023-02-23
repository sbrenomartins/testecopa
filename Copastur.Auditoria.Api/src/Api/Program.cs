using Application.Interfaces;
using Application.Services;

using Infra.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AuditoriaSettings>(
    builder.Configuration.GetSection("ConnectionStrings:Mongo"));

builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();
builder.Services.AddHostedService<RabbitMQConsumer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
