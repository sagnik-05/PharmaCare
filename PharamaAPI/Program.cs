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
using Microsoft.OpenApi.Models;
using PharmaAPI.Repositories;

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PharmaAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insert token",
        Name = "Auth",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new String[]{}
        }
    });
});

// Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});
// Register AuthRepository as the concrete implementation of IAuthService
builder.Services.AddScoped<IAuthService, AuthRepository>();
// Register InventoryRepository as the concrete implementation of IInventoryRepository  
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
// Register DrugRepository as the concrete implementation of IDrugService
builder.Services.AddScoped<IDrugRepository, DrugRepository>();
// Register SalesRepository as the concrete implementation of ISalesRepository
builder.Services.AddScoped<ISalesRepository, SalesRepository>(); 
// Register OrderRepository as the concrete implementation of IOrderRepository
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
// Register SupplierRepository as the concrete implementation of ISupplierRepository
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
