using System;
using System.Collections.Generic;

namespace Consorcio_Api.Models;

public partial class Department
{
    public int IdDepartment { get; set; }

    public string? Name { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
