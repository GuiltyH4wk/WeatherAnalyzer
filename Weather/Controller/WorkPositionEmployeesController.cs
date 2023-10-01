using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroesDatabase.DataBaseContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeroesDatabase.Model;

namespace HeroesDatabase.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPositionEmployeesController : ControllerBase
    {
        private readonly HeroesDatabaseContext _context;

        public WorkPositionEmployeesController(HeroesDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/WorkPositionEmployees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkPositionEmployee>>> GetWorkPositionEmployee()
        {
          if (_context.WorkPositionEmployee == null)
          {
              return NotFound();
          }

          List<Data.WorkPositionEmployee> workPositionEmployees = await _context.WorkPositionEmployee.ToListAsync();
          List< WorkPositionEmployee> workPositionEmployeesDTO = new List<WorkPositionEmployee>();

            foreach (Data.WorkPositionEmployee workPositionEmployee in workPositionEmployees)
            {
                WorkPositionEmployee workPositionEmployeeDTO = new WorkPositionEmployee();
                workPositionEmployeeDTO.Id = workPositionEmployee.Id;
                workPositionEmployeeDTO.WorkPositionId = workPositionEmployee.WorkPositionId;
                workPositionEmployeeDTO.EmployeeId = workPositionEmployee.EmployeeId;
                workPositionEmployeeDTO.Started = workPositionEmployee.Started;
                workPositionEmployeeDTO.Ended = workPositionEmployee.Ended;
                workPositionEmployeesDTO.Add(workPositionEmployeeDTO);
            }

            return workPositionEmployeesDTO;
        }

        // GET: api/WorkPositionEmployees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkPositionEmployee>> GetWorkPositionEmployee(Guid id)
        {
          if (_context.WorkPositionEmployee == null)
          {
              return NotFound();
          }
            var workPositionEmployee = await _context.WorkPositionEmployee.FindAsync(id);

            if (workPositionEmployee == null)
            {
                return NotFound();
            }

            WorkPositionEmployee workPositionEmployeeDTO = new WorkPositionEmployee();
            workPositionEmployeeDTO.Id = workPositionEmployee.Id;
            workPositionEmployeeDTO.WorkPositionId = workPositionEmployee.WorkPositionId;
            workPositionEmployeeDTO.EmployeeId = workPositionEmployee.EmployeeId;
            workPositionEmployeeDTO.Started = workPositionEmployee.Started;
            workPositionEmployeeDTO.Ended = workPositionEmployee.Ended;

            return workPositionEmployeeDTO;
        }

        // PUT: api/WorkPositionEmployees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkPositionEmployee(Guid id, WorkPositionEmployeePut workPositionEmployeePutDTO)
        {
            Data.WorkPositionEmployee workPositionEmployee = await _context.WorkPositionEmployee.FindAsync(id);

            workPositionEmployee.Started = workPositionEmployeePutDTO.Started;
            workPositionEmployee.Ended = workPositionEmployeePutDTO.Ended;

            _context.WorkPositionEmployee.Update(workPositionEmployee);
             await _context.SaveChangesAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkPositionEmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WorkPositionEmployees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkPositionEmployee>> PostWorkPositionEmployee(WorkPositionEmployeePersist workPositionEmployeePersistDTO)
        {
          if (_context.WorkPositionEmployee == null)
          {
              return Problem("Entity set 'HeroesDatabaseContext.WorkPositionEmployee'  is null.");
          }
 

            Data.WorkPositionEmployee workPositionEmployee = new Data.WorkPositionEmployee();
            workPositionEmployee.Id = Guid.NewGuid();
            workPositionEmployee.WorkPositionId = workPositionEmployeePersistDTO.WorkPositionId;
            workPositionEmployee.EmployeeId = workPositionEmployeePersistDTO.EmployeeId;
            workPositionEmployee.Started = workPositionEmployeePersistDTO.Started;
            workPositionEmployee.Ended = workPositionEmployeePersistDTO.Ended;


            _context.WorkPositionEmployee.Add(workPositionEmployee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkPositionEmployee", new { id = workPositionEmployee.Id }, workPositionEmployeePersistDTO);
        }

        // DELETE: api/WorkPositionEmployees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkPositionEmployee(Guid id)
        {
            if (_context.WorkPositionEmployee == null)
            {
                return NotFound();
            }
            var workPositionEmployee = await _context.WorkPositionEmployee.FindAsync(id);
            if (workPositionEmployee == null)
            {
                return NotFound();
            }

            _context.WorkPositionEmployee.Remove(workPositionEmployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkPositionEmployeeExists(Guid id)
        {
            return (_context.WorkPositionEmployee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
