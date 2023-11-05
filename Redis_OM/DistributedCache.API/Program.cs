using DistributedCache.API.Extensions;
using DistributedCache.API.MinimalApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCqrs();

builder.Services.AddConfiguration(builder.Configuration);

builder.Services.AddMsSqlDatabase();

builder.Services.AddRedisWithRedisOM();

builder.Services.AddExtraServices();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.InitDatabase();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Minimal Api
app.RegisterCustomersApi();
app.RegisterOrganizationsApi();

app.Run();