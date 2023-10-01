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
    public class WorkPositionsController : ControllerBase
    {
        private readonly IWorkPositionService _workPositionService;

        public WorkPositionsController(IWorkPositionService workPositionService)
        {
            _workPositionService = workPositionService;
        }

        // GET: api/WorkPositions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkPosition>>> GetWorkPosition()
        {
            return await this._workPositionService.GetWorkPosition();
        }

        // GET: api/WorkPositions/5
        [HttpGet("{workPositionId}")]
        public async Task<ActionResult<WorkPosition>> GetWorkPosition(Guid workPositionId)
        {
            return await this._workPositionService.GetWorkPosition(workPositionId);
        }

        // PUT: api/WorkPositions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{workPositionId}")]
        public async Task<IActionResult> PutWorkPosition(Guid workPositionId, WorkPositionPersist workPositionPersist)
        {

            await this._workPositionService.PutWorkPosition(workPositionId, workPositionPersist);
            return NoContent();
        }

        // POST: api/WorkPositions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkPosition>> PostWorkPosition(WorkPositionPersist workPositionPersist)
        {

            WorkPosition workPosition = new WorkPosition(); 

            return CreatedAtAction("GetWorkPosition", new { workPositionId = workPosition.Id }, workPosition);
        }

        // DELETE: api/WorkPositions/5
        [HttpDelete("{workPositionId}")]
        public async Task<IActionResult> DeleteWorkPosition(Guid workPositionId)
        {


            return NoContent();
        }


    }
}
