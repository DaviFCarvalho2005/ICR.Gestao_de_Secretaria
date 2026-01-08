using ICR.Application.Services;
using ICR.Application.ViewModel;
using ICR.Domain.Model.ChurchAggregate;
using ICR.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using ICR.Domain.Model.CellAggregate;

namespace ICRManagement.API.Controllers
{
    [ApiController]
    [Route("api/v1/churchs")]
    public class ChurchsController : ControllerBase
    {
        private readonly IChurchRepository _repository;
        private readonly IdSequenceService _seq;

        public ChurchsController(
            IChurchRepository repository,
            IdSequenceService seq)
        {
            _repository = repository;
            _seq = seq;
        }

        // CREATE
        [HttpPost]
        public IActionResult Create([FromBody] ChurchViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                return BadRequest("Invalid data");

            var Churchid = _seq.GetNextId<Church>();
            var Cellid = _seq.GetNextId<Cell>();
            var church = new Church(Churchid, model.Name, model.Address, model.FederationId, model.MinisterId);


            _repository.Add(church);
            return CreatedAtAction(nameof(GetById), new { Churchid }, null);
        }

        // GET ALL
        [HttpGet]
        [HttpGet]
        public IActionResult GetAll(int pageNumber = 1, int pageQuantity = 10)
        {
            var federations = _repository.Get(pageNumber, pageQuantity);

            var result = federations.Select(f => new ChurchViewModel
            {
                Id = f.Id,
                Name = f.Name,
                MinisterId = f.MinisterId
            }).ToList();

            return Ok(result);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var federation = _repository.GetById(id);
            if (federation == null) return NotFound();
            return Ok(federation);
        }

        // PATCH
        [HttpPatch("{id}")]
        public IActionResult Patch(
    [FromRoute] long id,
    [FromForm] ChurchDTO dto)
        {
            var church = _repository.GetById(id);
            if (church == null)
                return NotFound();
            if (dto.Name != null)
                church.SetName(dto.Name);
            if (dto.Address != null)
                church.SetAddress(dto.Address);
            if (dto.FederationId != null)
                church.SetFederationId(dto.FederationId);
            if (dto.MinisterId != null)
                church.SetMinisterId(dto.MinisterId);

            _repository.Save();
            return NoContent();
        }
        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _repository.Delete(id);
            return NoContent();
        }
    }
}
