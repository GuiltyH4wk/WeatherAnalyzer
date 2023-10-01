using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using HeroesDatabase.DataBaseContext;
using HeroesDatabase.Model;
using HeroesDatabase.Services;
using HeroesDatabase.Services.InterfaceService;

namespace HeroesDatabase.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchesController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Branch>>> GetBranch()
        {


            return await this._branchService.GetBranch(); 
        }

        // GET: api/Branches/5
        [HttpGet("{branchId}")]
        public async Task<ActionResult<Branch>> GetBranch(Guid branchId)
        {


            return await this._branchService.GetBranch(branchId);
        }

        // PUT: api/Branches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{branchId}")]
        public async Task<IActionResult> PutBranch(Guid branchId, BranchPersist branchPersist)
        {

            await this._branchService.PutBranch(branchId, branchPersist);

            return NoContent();
        }

        // POST: api/Branches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BranchPersist>> PostBranch(BranchPersist branchPersist)
        {

            Branch branch = await this._branchService.PostBranch(branchPersist);

            return CreatedAtAction("GetBranch", new { branchId = branch.Id }, branch);
        }

        // DELETE: api/Branches/5
        [HttpDelete("{branchId}")]
        public async Task<IActionResult> DeleteBranch(Guid branchId)
        {
            await this._branchService.DeleteBranch(branchId);
            return NoContent();
        }


    }
}
