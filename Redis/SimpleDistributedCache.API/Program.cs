using DistributedCache.API.Extensions;
using SimpleDistributedCache.API.Extensions;
using SimpleDistributedCache.API.MinimalApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCqrs();

builder.Services.AddConfiguration(builder.Configuration);

builder.Services.AddMsSqlDatabase();

builder.Services.AddRedisAsDistributedCache();

builder.Services.AddExtraServices();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.InitDatabase();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Minimal Api
app.RegisterOrganizationsApi();

app.Run();