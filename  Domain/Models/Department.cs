using System;
using System.Collections.Generic;

namespace Consorcio_Api.Domain.Models
{
    public partial class Department
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
