using Colony_Management_System.Services;
using Colony_Management_System.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Colony_Management_System.Models.DbContext;

var builder = WebApplication.CreateBuilder(args);
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
var connectionString = "Server=sql7.freesqldatabase.com;Database=your_database_name;User=your_username;Password=your_password;Port=3306;";
var context = new KoloniaDbContext(connectionString);
// Add services to the container.
builder.Services.AddControllersWithViews(); // Obs�uguje MVC (kontrolery + widoki)

// Dodajemy us�ugi wymagane do autentykacji i autoryzacji JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ColonyManagementSystem",
            ValidAudience = "ColonyManagementSystemAPI",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xLvOEFU2wtLi5tirZm7e6kGWC8txB3RhHF6JmeUJiBQ="))
        };
    });

// Dodajemy Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Colony Management API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
});

// Rejestracja serwis�w
builder.Services.AddTransient<IAccountRepository, AccountRepository>(); // Rejestracja repozytorium kont
builder.Services.AddTransient<IUserService, UserService>(); // Rejestracja serwisu autoryzacji u�ytkownika

// Zaktualizowana konfiguracja DbContext do MySQL
builder.Services.AddDbContext<KoloniaDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("Server=sql7.freesqldatabase.com;Database=your_database_name;User=your_username;Password=your_password;Port=3306;"),
    new MySqlServerVersion(new Version(5, 5, 62))));  // U�ywamy wersji MySQL 5.5.62

// Rejestracja innych serwis�w, kt�re mog� by� potrzebne
builder.Services.AddHttpContextAccessor(); // Je�li u�ywasz HttpContext w innych serwisach

var app = builder.Build();

// Configure the HTTP request pipeline.
#if DEBUG
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    try
    {
        List<string> url = File.ReadLines(Environment.CurrentDirectory + "/swagger.config").ToList();
        c.SwaggerEndpoint(url[0], "Colony Management API");
    }
    catch
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Colony Management API v1");
    }
    c.RoutePrefix = "swagger";
    c.ConfigObject.PersistAuthorization = true;
});
#endif

// Konfiguracja autentykacji i autoryzacji
app.UseAuthentication();
app.UseAuthorization();

// Mapowanie kontroler�w
app.MapControllers();

app.Run();
