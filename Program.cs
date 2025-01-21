
using Consorcio_Api.Application.Interfaces;
using Consorcio_Api.Application.Services;
using Consorcio_Api.Application.Services.EmployeeService;
using Consorcio_Api.Persistence;
using Consorcio_Api.Utilities;
using Microsoft.EntityFrameworkCore;

public class Program
{

    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
        

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        });



        builder.Services.AddCors(options =>
        {
            options.AddPolicy("New Policy", app =>
            {
                app.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
            });
        });
        

        builder.Services.AddDbContext<ConsorcioDbContext>(options =>
        {

            
           options.UseSqlServer(builder.Configuration.GetConnectionString("Development"));
        });
        builder.Services.AddScoped<IDepartment, DepartmentService>();
        builder.Services.AddScoped<IEmployee, EmployeeService>();

        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Consorcio_Api v2");
            });

        }

        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers(); 
        app.UseCors("New Policy");

        app.Run();
    }
}
