using ICRManagement.Domain.Model.FederationAggregate;
using ICRManagement.Domain.Repositories;
using ICRManagement.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ICRManagement.API.Controllers
{
    [ApiController]
    [Route("api/v1/federation")]
    public class FederationController : ControllerBase
    {
        private readonly IFederationRepository _repository;

        public FederationController(IFederationRepository repository)
        {
            _repository = repository;
        }

        // ------------------- CREATE -------------------
        // POST: api/v1/federation
        [HttpPost]
        public IActionResult Create([FromForm] FederationViewModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Name))
                return BadRequest("Invalid federation data");

            var nextSeq = _repository.GetNextSequence();
            var now = DateTime.UtcNow;
            var customId = $"ICR{now:yyyyMM}-{nextSeq}";

            var federation = new Federation(model.Name, customId);
            _repository.Add(federation);

            return CreatedAtAction(nameof(GetById), new { id = federation.Id }, federation);
        }

        // ------------------- GET ALL -------------------
        // GET: api/v1/federation?pageNumber=1&pageQuantity=10
        [HttpGet]
        public IActionResult GetAll(int pageNumber = 1, int pageQuantity = 10)
        {
            var federations = _repository.Get(pageNumber, pageQuantity);
            return Ok(federations);
        }

        // ------------------- GET BY ID -------------------
        // GET: api/v1/federation/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var federation = _repository.Get(id);
            if (federation == null) return NotFound();

            return Ok(federation);
        }

        // ------------------- UPDATE -------------------
        // PUT: api/v1/federation/{id}
        [HttpPatch("{id}")]
        public IActionResult Update(string id, [FromForm] FederationViewModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Name))
                return BadRequest("Invalid data");

            var federation = _repository.Get(id);
            if (federation == null) return NotFound();

            federation.SetName(model.Name);
            _repository.UpdateName(federation.Id, model.Name);

            return NoContent();
        }

        // ------------------- DELETE -------------------
        // DELETE: api/v1/federation/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var federation = _repository.Get(id);
            if (federation == null) return NotFound();

            _repository.Delete(federation.Id);
            return NoContent();
        }
    }
}
