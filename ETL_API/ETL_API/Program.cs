using ETL_API.HostedServices;
using ETL_API.Repositories.Interfaces;
using ETL_API.Repositories;
using ETL_API.Services;
using Microsoft.EntityFrameworkCore;
using ETL_Shared.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PatientDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("db"), ma => ma.MigrationsAssembly("ETL_API")));
builder.Services.AddScoped<MigrationAndSeederService>();
builder.Services.AddHostedService<MigrationAndSeederHostedService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(
    settings =>
    {
        settings.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
        settings.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
    }
    );
builder.Services.AddSwaggerGen();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
