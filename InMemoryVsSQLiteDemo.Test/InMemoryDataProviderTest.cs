using System;
using System.Linq;
using SmartIT.Employee.MockDB;
using Xunit;

namespace InMemoryVsSQLiteDemo.Test
{
    public class InMemoryDataProviderTest
    {
        [Fact]
        public void Task_Add_Without_Relation()
        {
            //Arrange  
            var factory = new ConnectionFactory();

            //Get the instance of employeeDBContext
            var context = factory.CreateContextForInMemory();

            var employee = new Employee() { Name = "Test Name 1", DepartmentId = 1, Gender = "Female", Salary = 11111};

            //Act  
            var data = context.Employee.Add(employee);
            context.SaveChanges();

            //Assert  
            //Get the employee count
            var employeeCount = context.Employee.Count();
            if (employeeCount != 0)
            {
                Assert.Equal(1, employeeCount);
            }

            //Get single employee detail
            var singleEmployee = context.Employee.FirstOrDefault();
            if (singleEmployee != null)
            {
                Assert.Equal("Test Name 1", singleEmployee.Name);
            }
        }

        [Fact]
        public void Task_Add_With_Relation()
        {
            //Arrange  
            var factory = new ConnectionFactory();

            //Get the instance of employeeDBContext
            var context = factory.CreateContextForInMemory();

            var employee = new Employee() { Name = "Test Name 3", Gender= "Female", DepartmentId = 2, Salary = 22222 };

            //Act  
            var data = context.Employee.Add(employee);
            context.SaveChanges();

            //Assert  
            //Get the employee count
            var employeeCount = context.Employee.Count();
            if (employeeCount != 0)
            {
                Assert.Equal(1, employeeCount);
            }

            //Get single employee detail
            var singleEmployee = context.Employee.FirstOrDefault();
            if (singleEmployee != null)
            {
                Assert.Equal("Test Name 3", singleEmployee.Name);
            }
        }

        [Fact]
        public void Task_Add_Time_Test()
        {
            //Arrange  
            var factory = new ConnectionFactory();

            //Get the instance of employeeDBContext
            var context = factory.CreateContextForInMemory();

            //Act 
            for (int i = 1; i <= 1000; i++)
            {
                //var employee = new employee() { Title = "Test Title " + i, Description = "Test Description " + i, CategoryId = 2, CreatedDate = DateTime.Now };
                //context.employee.Add(employee);

                //var context = factory.CreateContextForInMemory();

                var employee = new Employee() { Name = "Test Name " +i, Gender = "Female " +i, DepartmentId = 2, Salary = 22222 };
                context.Employee.Add(employee);
            }

            context.SaveChanges();


            //Assert  
            //Get the employee count
            var employeeCount = context.Employee.Count();
            if (employeeCount != 0)
            {
                Assert.Equal(1000, employeeCount);
            }

            //Get single employee detail
            var singleEmployee = context.Employee.Where(x => x.Id == 1).FirstOrDefault();
            if (singleEmployee != null)
            {
                Assert.Equal("Test Name 1", singleEmployee.Name);
            }
        }
    }
}
