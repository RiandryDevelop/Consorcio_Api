
using AutoMapper;
using Consorcio_Api.Application.Interfaces;
using Consorcio_Api.Application.Services;
using Consorcio_Api.Application.Services.EmployeeService;
using Consorcio_Api.Infrastructure.Persistence;
using Consorcio_Api.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(proveedor => new MapperConfiguration(configuracion =>
{
    var geometryFactory = proveedor.GetRequiredService<GeometryFactory>();
    configuracion.AddProfile(new AutoMapperProfiles(geometryFactory));
}).CreateMapper());

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<ConsorcioDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();

builder.Services.AddAuthentication().AddJwtBearer(opciones =>
{
    opciones.MapInboundClaims = false;

    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["llavejwt"]!)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(opciones =>
{
    opciones.AddPolicy("esadmin", politica => politica.RequireClaim("esadmin"));
});

builder.Services.AddDbContext<ConsorcioDbContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection", sqlServer =>
    sqlServer.UseNetTopologySuite()));

builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));

builder.Services.AddOutputCache(opciones =>
{
    opciones.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(60);
});

var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!.Split(",");

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(opcionesCORS =>
    {
        opcionesCORS.WithOrigins(origenesPermitidos).AllowAnyMethod().AllowAnyHeader()
        .WithExposedHeaders("cantidad-total-registros");
    });
});

builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosAzure>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IServicioUsuarios, ServicioUsuarios>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.UseOutputCache();


app.Run();

//public class Program
//{

//    public static void Main(string[] args)
//    {

//        var builder = WebApplication.CreateBuilder(args);


//        // Add services to the container.
//        builder.Services.AddEndpointsApiExplorer();
//        builder.Services.AddSwaggerGen();
//        builder.Services.AddControllers();
//        builder.Services.AddSingleton(proveedor => new MapperConfiguration(configuracion =>
//        {
//            var geometryFactory = proveedor.GetRequiredService<GeometryFactory>();
//            configuracion.AddProfile(new AutoMapperProfiles(geometryFactory));
//        }).CreateMapper());

//        builder.Services.AddIdentityCore<IdentityUser>()
//            .AddEntityFrameworkStores<ConsorcioDbContext>()
//            .AddDefaultTokenProviders();


//        builder.Services.AddCors(options =>
//        {
//            options.AddPolicy("New Policy", app =>
//            {
//                app.AllowAnyOrigin()
//                   .AllowAnyHeader()
//                   .AllowAnyMethod();
//            });
//        });


//        builder.Services.AddDbContext<ConsorcioDbContext>(options =>
//        {


//           options.UseSqlServer(builder.Configuration.GetConnectionString("Development"));
//        });


//        // Add Scoped Services
//        builder.Services.AddScoped<UserManager<IdentityUser>>();
//        builder.Services.AddScoped<SignInManager<IdentityUser>>();
//        builder.Services.AddScoped<IDepartment, DepartmentService>();
//        builder.Services.AddScoped<IEmployee, EmployeeService>();

//        // Add Authentication Services
//        builder.Services.AddAuthentication()
//            .AddJwtBearer(options =>
//            {
//                options.MapInboundClaims = false;
//                options.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuer = false,
//                    ValidateAudience = false,
//                    ValidateLifetime = true,
//                    ValidateIssuerSigningKey = true,                    
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt_Key"])),
//                    ClockSkew = TimeSpan.Zero
//                };
//            });


//        var app = builder.Build();

//        if (app.Environment.IsDevelopment())
//        {
//            app.UseSwagger();
//            app.UseSwaggerUI(c =>
//            {
//                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Consorcio_Api v2");
//            });

//        }

//        app.UseRouting();
//        app.UseAuthorization();
//        app.MapControllers(); 
//        app.UseCors("New Policy");

//        app.Run();
//    }
//}
