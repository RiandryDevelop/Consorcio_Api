using AutoMapper;
using Consorcio_Api.Application.DTOs;
using Consorcio_Api.Domain.Models;
using System.Globalization;

namespace Consorcio_Api.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Department, DepartmentDTO>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.HasValue ? src.CreationDate.Value.ToString("MM/dd/yyyy") : null))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdateDate.HasValue ? src.UpdateDate.Value.ToString("MM/dd/yyyy") : null))
                .ReverseMap()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.CreationDate) ? DateTime.ParseExact(src.CreationDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.UpdateDate) ? DateTime.ParseExact(src.UpdateDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null));

            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.DepartmentNavigation.DepartmentName))
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate.ToString("MM/dd/yyyy")));

            CreateMap<EmployeeDTO, Employee>()
                .ForMember(dest => dest.DepartmentNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.HireDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
