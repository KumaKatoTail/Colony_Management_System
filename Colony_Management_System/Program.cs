using Colony_Management_System.Services;
using Colony_Management_System.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Colony_Management_System.Models.DbContext;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

builder.Services.AddDbContext<KoloniaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 3, 0)) // Wersja MySQL
    )
);

// Dodaj us³ugi do kontenera
builder.Services.AddControllersWithViews(); // Obs³uguje MVC (kontrolery + widoki)

// Dodaj us³ugi autentykacji i autoryzacji JWT
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperLongSecretKeyThatIsAtLeast32BytesLong12345")) // Klucz podpisu JWT
        };
    });

// Dodaj Swagger
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

    // Dodaj konfiguracjê dla Swagger UI OAuth2Redirect
    option.OperationFilter<SwaggerJwtOperationFilter>();
});

// Rejestracja serwisów
builder.Services.AddTransient<IAccountRepository, AccountRepository>(); // Rejestracja repozytorium kont
builder.Services.AddTransient<IUserService, UserService>(); // Rejestracja serwisu autoryzacji u¿ytkownika
builder.Services.AddTransient<IKoloniaService, KoloniaService>();
builder.Services.AddTransient<IKoloniaRepository, KoloniaRepository>();
builder.Services.AddTransient<IDzieckoService, DzieckoService>();
builder.Services.AddTransient<IDzieckoRepository, DzieckoRepository>();
builder.Services.AddTransient<IGrupaRepository, GrupaRepository>();
builder.Services.AddTransient<IGrupaService, GrupaService>();
builder.Services.AddTransient<IKontoRepository, KontoRepository>();
builder.Services.AddTransient<IKontoService, KontoService>();
builder.Services.AddTransient<IKontoRepository2, KontoRepository2>();
builder.Services.AddTransient<IKontoService2, KontoService2>();




builder.Services.AddHttpContextAccessor(); // Jeœli u¿ywasz HttpContext w innych serwisach

var app = builder.Build();

// Konfiguracja HTTP request pipeline
#if DEBUG
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Colony Management API v1");
    c.RoutePrefix = string.Empty; // Swagger UI jako strona domyœlna
    c.ConfigObject.PersistAuthorization = true;

    // Wstrzykniêcie niestandardowego skryptu JavaScript
    c.InjectJavascript("swagger-ui/custom-swagger.js");
});
#endif

// Konfiguracja autentykacji i autoryzacji
app.UseAuthentication();
app.UseAuthorization();

// Mapowanie kontrolerów
app.MapControllers();

app.Run();

// Klasa operacji Swaggera dla automatycznego logowania
public class SwaggerJwtOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.OperationId == "Authenticate") // Nazwa metody logowania
        {
            operation.Responses.Add("200", new OpenApiResponse
            {
                Description = "Token retrieved and applied to Swagger UI"
            });
        }
    }
}
