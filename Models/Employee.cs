using System;
using System.Collections.Generic;

namespace Consorcio_Api.Models;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public string? FullName { get; set; }

    public int? IdDepartment { get; set; }

    public int? Salary { get; set; }

    public DateTime? ContractDate { get; set; }

    public virtual Department IdDepartmentNavigation { get; set; }
}
