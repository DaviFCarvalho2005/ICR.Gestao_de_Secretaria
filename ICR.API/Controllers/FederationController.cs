using ICR.Application.Services;
using ICR.Application.ViewModel;
using ICR.Domain.Model.FederationAggregate;
using ICR.Infra.Repositories;
using ICRManagement.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FederationController : ControllerBase
    {
        private readonly IFederationRepository _repository;

        public FederationController(IFederationRepository repository)
        {
            _repository = repository;
        }

        // GET: api/federation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Federation>>> GetAll()
        {
            var federations = await _repository.GetAllFederationsAsync();
            return Ok(federations);
        }

        // GET: api/federation/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Federation>> GetById(long id)
        {
            var federation = await _repository.GetByIdAsync(id);
            if (federation == null)
                return NotFound();
            return Ok(federation);
        }

        // POST: api/federation
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Federation>>> Create([FromForm] FederationViewModel model)
        {
            var federations = new Federation(0, model.Name, model.MinisterId);
                
            await _repository.AddAsync(federations);
            return Ok(new
            {
                federations.Name,
                federations.MinisterId
            });
        }

        // PUT: api/federation/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] FederationDTO updatedFederation)
        {
            var federation = await _repository.GetByIdAsync(id);
            if (federation == null) return NotFound();
            federation.SetName(updatedFederation.Name);
            federation.SetPastorId(updatedFederation.PastorId);
            _repository.UpdateAsync(federation);

            return NoContent();
        }

        // DELETE: api/federation/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var federation = await _repository.GetByIdAsync(id);
            if (federation == null)
                return NotFound();

            _repository.DeleteAsync(federation);
            return NoContent();
        }
    }
}
