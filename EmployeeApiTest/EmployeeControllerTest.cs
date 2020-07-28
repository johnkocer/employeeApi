using System.Threading.Tasks;
using EmployeeWebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using SmartIT.Employee.MockDB;
using Xunit;

namespace EmployeeApiTest
{
    public class EmployeeControllerTest
    {
        public EmployeeControllerTest()
        {
            _service = new EmployeeServiceFake();
            _controller = new EmployeeController(_service);
        }

        private readonly EmployeeController _controller;
        private readonly IEmployeeRepository _service;

        //[Fact]
        //public void GetById_ExistingGuidPassed_ReturnsRightItem()
        //{
        //    // Arrange
        //    var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

        //    // Act
        //    var okResult = _controller.Get(testGuid).Result as OkObjectResult;

        //    // Assert
        //    Assert.IsType<ShoppingItem>(okResult.Value);
        //    Assert.Equal(testGuid, (okResult.Value as ShoppingItem).Id);
        //}

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new Employee()
            {
                Name = "Guinness",
                Salary = 3333
            };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.Post(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        [Fact]
        public async Task Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new Employee()
            {
                Name = "Guinness Original 6 Pack",
                Salary = 5555,
                Gender = "Male",
                DepartmentId = 1
            };

            // Act
            var createdResponse = await _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Employee;

            // Assert
            Assert.IsType<Employee>(item);
            Assert.Equal("Guinness Original 6 Pack", item.Name);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var testItem = new Employee()
            {
                Name = "Guinness Original 6 Pack",
                Salary = 11111,
                Gender = "Female",
                DepartmentId = 1
            };

            // Act
            var createdResponse = _controller.Post(testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public async void Task_GetPostById_Return_OkResult()
        {
            //Arrange  
            var controller = new EmployeeController(_service);
           // var postId = 2;

            var testItem = new Employee()
            {
                Name = "Guinness Original 6 Pack",
                Salary = 11111,
                Gender = "Female",
                DepartmentId = 1
            };

            //Act  
            var data = await controller.Post(testItem);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = await _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingIDPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = 2;

            // Act
            var okResult = _controller.Get(2.ToString());

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        //[Fact]
        //public void Get_WhenCalled_ReturnsAllItems()
        //{
        //    // Act
        //    var okResult =   _controller.Get().Result as OkObjectResult;

        //    // Assert
        //    var items = Assert.IsType<List<Employee>>(okResult.Value);
        //    Assert.Equal(3, items.Count);
        //}

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get("9999");

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var existingGuid = 4;

            // Act
            var okResponse = _controller.Delete(existingGuid);

            // Assert
            Assert.Equal(3, _service.GetAll().Count);
        }

       

        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var existingGuid = 4;

            // Act
            var okResponse = _controller.Delete(existingGuid);

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingGuid = 444;

            // Act
            var badResponse = _controller.Delete(notExistingGuid);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }
    }
}