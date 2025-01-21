namespace Consorcio_Api.Application.DTOs
{
    public class EmployeeDTO
    {
        public int IdEmployee { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }
        public string? HireDate { get; set; }
        public bool IsActive { get; set; }
        public int? IdDepartment { get; set; }
        public string? DepartmentName { get; set; }
    }
}
