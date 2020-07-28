using System;
using EmployeeWebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SmartIT.Employee.MockDB;
using Xunit;

namespace EmployeeApiTest
{
    public class EmployeeControllerTest02
    {
        private readonly EmployeeController _controller;
        private readonly IEmployeeRepository _service;
        public EmployeeControllerTest02()
        {
            _service = new EmployeeServiceFake();
            _controller = new EmployeeController(_service);
        }

       

        [Fact]
        public async void Task_Add_InvalidData_Return_BadRequest()
        {
            //Arrange  
            var controller = new EmployeeController(_service);
            var testItem = new Employee
            {
                Id = 1,
                Name = "John Smith",
                Salary = 5555,
                Gender = "Male",
                DepartmentId = 1
            };

            //Act              
            var data1 = await controller.Post(testItem);
            var data = await controller.Post(testItem);

            //Assert  
            Assert.IsType<CreatedAtActionResult>(data);
        }

        [Fact]
        public async void Task_Add_ValidData_MatchResult()
        {
            //Arrange  
            var controller = new EmployeeController(_service);
            var testItem = new Employee
            {
                Name = "John Smith",
                Salary = 5555,
                Gender = "Male",
                DepartmentId = 1
            };

            //Act  
            var data = await controller.Post(testItem);

            //Assert  
            Assert.IsType<CreatedAtActionResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            // var result = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;  

            Assert.Equal(3, okResult.Value);
        }

        [Fact]
        public async void Task_Add_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new EmployeeController(_service);
            var testItem = new Employee
            {
                Name = "John Smith",
                Salary = 5555,
                Gender = "Male",
                DepartmentId = 1
            };

            //Act  
            var data = await controller.Post(testItem);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_GetPostById_MatchResult()
        {
            //Arrange  
            var controller = new EmployeeController(_service);
            int? employeeId = 1;
            var testItem = new Employee
            {
                Name = "John Smith",
                Salary = 5555,
                Gender = "Male",
                DepartmentId = 1
            };
            var data1 = await controller.Post(testItem);

            //Act  
            var data = controller.Get(data1.ToString());

            //Assert  
            Assert.IsType<ActionResult>(data.Value);

            var okResult = data.Should().BeOfType<ActionResult>().Subject;
            var post = okResult.Should().BeAssignableTo<Employee>().Subject;

            Assert.Equal("John Smith", post.Name);
            //Assert.Equal("John Smith", post.Name);
        }

        [Fact]
        public async void Task_GetPostById_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new EmployeeController(_service);
            //var emmloyeeId = null;
            int? employeeId = null;

            //Act  
            var data = controller.Get(employeeId.ToString());

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async void Task_GetPostById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new EmployeeController(_service);
            var emmloyeeId = 100;

            //Act  
            var data = controller.Get(100.ToString());

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }


        [Fact]
        public async void Task_GetPostById_Return_OkResult()
        {
            //Arrange  
            var controller = new EmployeeController(_service);
            var emmloyeeId = 2;

            // Arrange
            var testItem = new Employee
            {
                Name = "John Smith",
                Salary = 5555,
                Gender = "Male",
                DepartmentId = 1
            };

            //Act  
            var data = await controller.Post(testItem);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }
    }
}