using PharmacyApi.Entity;
using PharmacyApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IGeneriRepository<Medicine>>(sp =>
    new GenericRepository<Medicine>(sp.GetRequiredService<IWebHostEnvironment>(), "medicines.json"));

builder.Services.AddSingleton<IGeneriRepository<Sale>>(sp =>
    new GenericRepository<Sale>(sp.GetRequiredService<IWebHostEnvironment>(), "sales.json"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "PharmacyApi v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
