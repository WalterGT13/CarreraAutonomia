using CarreraAutonomiaAPI.Data;
using CarreraAutonomiaAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:9000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// 1. Conexión a la base de datos MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))));

// 2. Inyección de servicios
builder.Services.AddScoped<IRegistroService, RegistroService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();


// 3. Servicios generales
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// 4. Middlewares
app.UseHttpsRedirection(); // Redirección HTTPS
app.UseCors("PermitirFrontend");


// 5. Swagger (activado en todos los entornos)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carrera API v1");
    c.RoutePrefix = "swagger"; // Disponible en: http://localhost:puerto/swagger
});

// 6. Seguridad básica
app.UseAuthorization();

// 7. Mapear controladores
app.MapControllers();

// 8. Ejecutar aplicación
app.Run();
