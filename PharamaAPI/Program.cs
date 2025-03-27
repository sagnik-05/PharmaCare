using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PharmaAPI.Models;
using PharmaAPI.Services;
using PharmaAPI.Interface;
using PharmaAPI.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Configure Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Configure Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 🔹 Add Authentication & JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// 🔹 Add Authorization (Fix)
builder.Services.AddAuthorization();
// Register AuthRepository as the concrete implementation of IAuthService
builder.Services.AddScoped<IAuthService, AuthRepository>();
// Register InventoryRepository as the concrete implementation of IInventoryRepository  
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

// Register AuthService separately (without injecting itself)
builder.Services.AddScoped<AuthService>();
var app = builder.Build();

// 🔹 Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 Configure Middleware
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
