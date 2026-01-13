using ICR.Domain.Model.FamilyAggregate;
using Microsoft.AspNetCore.Mvc;

namespace ICRManagement.API.Controllers
{
    [ApiController]
    [Route("api/v1/families")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyRepository _repository;

        public FamilyController(IFamilyRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Family model)
        {
            if (model == null) return BadRequest();
            _repository.Add(model);
            _repository.Save();
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, null);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var family = _repository.GetById(id);
            if (family == null) return NotFound();
            return Ok(family);
        }

        [HttpGet]
        public IActionResult GetAll(int pageNumber = 1, int pageQuantity = 10)
        {
            var list = _repository.Get(pageNumber, pageQuantity);
            return Ok(list);
        }

        [HttpGet("by-member/{memberId}")]
        public IActionResult GetByMember(long memberId)
        {
            var list = _repository.GetFamiliesByWeddingBirthdayMonth(memberId);
            return Ok(list);
        }

        [HttpGet("by-cell/{cellId}")]
        public IActionResult GetByCell(long cellId)
        {
            var list = _repository.GetbyCellId(cellId);
            return Ok(list);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _repository.Delete(id);
            _repository.Save();
            return NoContent();
        }
    }
}
