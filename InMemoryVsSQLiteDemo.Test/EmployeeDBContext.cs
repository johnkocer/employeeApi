using System;
using Microsoft.EntityFrameworkCore;
using SmartIT.Employee.MockDB;
using Xunit;

namespace InMemoryVsSQLiteDemo.Test
{
    public partial class EmployeeDBContext:DbContext
    {
        public EmployeeDBContext() { }

        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
            : base(options) { }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Department> Department { get; set; }
    }
}
