using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Consorcio_Api.Application.Interfaces;
using Consorcio_Api.Application.DTOs;

namespace Consorcio_Api.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartment departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetDepartments()
        {
            var departmentList = await _departmentService.GetList();
            var departmentListDTO = _mapper.Map<List<DepartmentDTO>>(departmentList);

            if (departmentListDTO.Count > 0)
                return Ok(departmentListDTO);
            else
                return NotFound();
        }
    }
}
