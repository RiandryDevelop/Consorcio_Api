using AutoMapper;
using Consorcio_Api.DTOs;
using Consorcio_Api.Models;
using System.Globalization;

namespace Consorcio_Api.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Department, DepartmentDTO>().ReverseMap();

            CreateMap<Employee, EmployeeDTO>()
                .ForPath(destination => destination.DepartmentName, option => option
                    .MapFrom(origen => origen.IdDepartmentNavigation.Name))
                .ForPath(destination => destination.ContractDate, option => option
                    .MapFrom(origen => origen.ContractDate.Value.ToString("MM/dd/yyyy")));

            CreateMap<EmployeeDTO, Employee>()
                .ForPath(destination => destination.IdDepartmentNavigation.Name, option => option
                    .Ignore())
                .ForPath(destination => destination.ContractDate, option => option
                    .MapFrom(origen => DateTime.ParseExact(origen.ContractDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)));

            
        }
    }
}
