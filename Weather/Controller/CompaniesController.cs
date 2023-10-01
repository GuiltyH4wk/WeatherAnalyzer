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
using HeroesDatabase.Services;

namespace HeroesDatabase
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompany()
        {
            return await _companyService.GetCompany();
        }

        // GET: api/Companies/5
        [HttpGet("{companyId}")]
        public async Task<ActionResult<Company>> GetCompany(Guid  companyId)
        {
            return await this._companyService.GetCompany(companyId);
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(Guid companyId, CompanyPersist companyPersist)
        {

            await _companyService.PutCompany(companyId, companyPersist);
            return NoContent();
        }

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(CompanyPersist companyPersist)
        {
            Company company = await _companyService.PostCompany(companyPersist);

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            await _companyService.DeleteCompany(companyId);

            return NoContent();
        }

    }
}
