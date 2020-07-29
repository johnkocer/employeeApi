using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartIT.Employee.MockDB;

namespace EmployeeWebApi.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeService;

        public EmployeeController(IEmployeeRepository employeeService)
        {
            _employeeService = employeeService;
        }

        [Route("/api/EmployeesById/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            //return await _employeeService.GetAllAsync();
            var items = await _employeeService.GetAllAsync();
            return Ok(items);
        }

        [Route("/api/EmployeesByName/{id}")]
        [Route("{id}")]
        [HttpGet]
        public ActionResult<Employee> Get(string id)
        {
            if (id.IsNumeric())
            {
                var item = _employeeService.FindbyId(Convert.ToInt32(id));
                if (item == null || item.Count == 0)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            var itemByFindbyName = _employeeService.FindbyName(id);
            if (itemByFindbyName == null)
            {
                return NotFound();
            }
            return Ok(itemByFindbyName);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Employee value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await _employeeService.AddAsync(value);
            return CreatedAtAction("Get", new {id = item.Id}, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Put([FromBody] Employee item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var returnItem  = await _employeeService.UpdateAsync(item);
           return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var findEmployee = await _employeeService.FindbyIdAsync(id);
            if (findEmployee == null)
                return NotFound();

            await _employeeService.DeleteAsync(findEmployee);

            return Ok();
        }
    }
}