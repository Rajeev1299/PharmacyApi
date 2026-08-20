using PharmacyApi.Entity;
using PharmacyApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IGeneriRepository<Medicine>>(sp =>
    new GenericRepository<Medicine>(sp.GetRequiredService<IWebHostEnvironment>(), "medicines.json"));

builder.Services.AddSingleton<IGeneriRepository<Sale>>(sp =>
    new GenericRepository<Sale>(sp.GetRequiredService<IWebHostEnvironment>(), "sales.json"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
