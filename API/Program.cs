using Database.Context;
using Database.Factories;
using DOCApiTier.Logic;
using DOCApiTier.Logic.LogicInterfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        builderOptions => builderOptions.MigrationsAssembly("API")));

builder.WebHost.ConfigureKestrel(options =>
{
    // Apply Kestrel configuration from appsettings.json
    options.Configure(builder.Configuration.GetSection("Kestrel"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var dbMigrationsManager = app.Services.GetRequiredService<DatabaseContext>();

if (dbMigrationsManager.Database.GetPendingMigrations().Any())
{
    dbMigrationsManager.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();