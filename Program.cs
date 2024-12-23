using Consorcio_Api;
using Consorcio_Api.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;

using Consorcio_Api.Services.Contract;
using Consorcio_Api.Services.Implementation;

using AutoMapper;
using Consorcio_Api.DTOs;
using Consorcio_Api.Utilities;

public class Program
{

    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
        

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
        

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("New Policy", app =>
            {
                app.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
            });
        });
        

        builder.Services.AddDbContext<DbemployeesContext>(options =>
        {

            
           options.UseSqlServer(builder.Configuration.GetConnectionString("Development"));
        });
        builder.Services.AddScoped<IDepartmentService, DepartmentService>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();

        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        //else
        //{
          //  builder.Services.AddDbContext<DbemployeesContext>(options =>
          //  {
           //     options.UseSqlServer(builder.Configuration.GetConnectionString("Production"));
           // });
        //}

        #region Api rest requests
        app.MapGet("/department/list", async (
            IDepartmentService _departmentService,
            IMapper _mapper
            ) =>
        {
            var departmentList = await _departmentService.GetList();
            var departmentListDTO = _mapper.Map<List<DepartmentDTO>>(departmentList);

            if (departmentListDTO.Count > 0)
                return Results.Ok(departmentListDTO);
            else
                return Results.NotFound();
        });

        app.MapGet("/employee/list", async (
            IEmployeeService _employeeService,
            IMapper _mapper
            ) =>
        {
            var employeeList = await _employeeService.GetList();
            var employeeListDTO = _mapper.Map<List<EmployeeDTO>>(employeeList);

            if (employeeListDTO.Count > 0)
                return Results.Ok(employeeListDTO);
            else
                return Results.NotFound();
        });

        app.MapPost("/employee/save", async (
            EmployeeDTO model,
            IEmployeeService _employeeService,
            IMapper _mapper
            ) =>
        {
            var _employee = _mapper.Map<Employee>(model);
            var _employeeeCreate = await _employeeService.Add(_employee);

            if (_employeeeCreate.IdEmployee != 0)
            {
                return Results.Ok(_mapper.Map<EmployeeDTO>(model));
            }
            else
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }

        });

        app.MapPut("/employee/update/{IdEmployee}", async (
            int IdEmployee,
            EmployeeDTO model,
            IEmployeeService _employeeService,
            IMapper _mapper
            ) =>
        {
            var _foundOne = await _employeeService.Get(IdEmployee);
            if (_foundOne is null) return Results.NotFound();
            var _employee = _mapper.Map<Employee>(model);
            _foundOne.FullName = _employee.FullName;
            _foundOne.Salary = _employee.Salary;
            _foundOne.ContractDate = _employee.ContractDate;
            _foundOne.IdDepartment = _employee.IdDepartment;

            var respond = await _employeeService.Update(_foundOne);

            if (respond)
                return Results.Ok(_mapper.Map<EmployeeDTO>(_foundOne));
            else
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
        });
        app.MapDelete("/employee/delete/{IdEmployee}", async (
            int IdEmployee,
            IEmployeeService _employeeService
            ) =>
        {
            var _foundOne = await _employeeService.Get(IdEmployee);

            if (_foundOne is null) return Results.NotFound();

            var respond = await _employeeService.Delete(_foundOne);

            if (respond)
                return Results.Ok();
            else
                return Results.NotFound(StatusCodes.Status500InternalServerError);
        });
        #endregion

        app.UseCors("New Policy");

        app.Run();
    }
}
