using backnc.Data.Context;
using backnc.Data.Interface;
using backnc.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backnc.Interfaces;
using backnc.Data.Seed;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno desde archivo .env
Env.Load();

// Obtener la cadena de conexión del archivo de configuración
string connectionString = builder.Configuration.GetConnectionString("defaultConnection");

// Reemplazar las variables de entorno en la cadena de conexión
connectionString = connectionString.Replace("${DB_SERVER}", Env.GetString("DB_SERVER"))
                                   .Replace("${DB_NAME}", Env.GetString("DB_NAME"));
								   
// Configurar el DbContext con la cadena de conexión
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Agregar otros servicios al contenedor
builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IProvinceSerivce, ProvinceService>();
builder.Services.AddScoped<INeighborhoodService, NeighborhoodService>();
builder.Services.AddScoped<IUserValidationService, UserValidationService>();
builder.Services.AddScoped<ProfileService, ProfileService>();
builder.Services.AddScoped<JobService, JobService>();
builder.Services.AddScoped<AdministradorService, AdministradorService>();
builder.Services.AddScoped<ClienteService, ClienteService>();
builder.Services.AddScoped<DataSeeder>();


builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "MercadoChamba!", Version = "v1" });

	// Configuración de seguridad JWT
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Por favor, ingrese el token JWT en este formato: Bearer {token}",
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement {
		{
			new OpenApiSecurityScheme {
				Reference = new OpenApiReference {
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
	c.EnableAnnotations();

});


builder.Services.AddControllers()
	.AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin",
		builder =>
		{
			builder.WithOrigins("http://localhost:4200")
				   .AllowAnyHeader()
				   .AllowAnyMethod()
				   .AllowCredentials();


		});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var seeder = services.GetRequiredService<DataSeeder>();
	await seeder.SeedAsync();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(@"C:\Users\Mateo\Desktop\mercadochamba\images"),	
	RequestPath = "/images"
});


app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
