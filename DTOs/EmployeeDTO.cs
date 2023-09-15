namespace Consorcio_Api.DTOs
{
    public class EmployeeDTO
    {
        public int IdEmployee { get; set; }

        public string? FullName { get; set; }

        public int? IdDepartment { get; set; }
        public string? DepartmentName { get; set; }

        public int? Salary { get; set; }

        public string? ContractDate { get; set; }
    }
}
