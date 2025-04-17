using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using POS.Api.Data;
using POS.Api.Interfaces;
using POS.Api.Models;
using POS.Api.Repositories;
using POS.Api.Utils;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


#region KONFIGURASI KONEKSI DATABASE
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region KONFIGURASI INDENTITY AUTHENTIKASI DAN OTORISASI
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // skema autentikasi
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // tantangan autentikasi
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // validasi penerbit token
        ValidateAudience = true, // validasi audiens token
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // penerbit token
        ValidAudience = builder.Configuration["Jwt:Audience"], // audiens token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])), // kunci untuk menandatangani token
        ClockSkew = TimeSpan.Zero // waktu toleransi untuk validasi token
    };
});

builder.Services.AddAuthorization(); // menambahkan otorisasi
#endregion

#region DAFTARKAN INTERFACE DAN REPOSITORY
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
#endregion

#region DAFTARKAN AUTOMAPPER
builder.Services.AddAutoMapper(typeof(Program));
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast =  Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");

app.Run();

using (var scope = app.Services.CreateScope()) //// Create a scope for the service provider
{
    await DbInitializer.SeedRoleAsync(scope.ServiceProvider); // Seed roles
}

//record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
