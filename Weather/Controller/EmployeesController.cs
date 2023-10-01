using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroesDatabase.DataBaseContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeroesDatabase.Model;
using HeroesDatabase.Services.InterfaceService;

namespace HeroesDatabase.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employees

        //[HttpPost] (offest size, vale mono lastname)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
             //Todo.Skip(0).Take(10).OrderBy(X => X.CreatedAt).ToListAsync();
            return await this._employeeService.GetEmployee();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            return await this._employeeService.GetEmployee(id);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, EmployeePersist employeePersist)
        {
            
           await _employeeService.PutEmployee(id, employeePersist);
            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeePersist employeePersist)
        {

            Employee employee =await _employeeService.PostEmployee(employeePersist);
            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _employeeService.DeleteEmployee(id);

            return NoContent();
        }
    }
}
