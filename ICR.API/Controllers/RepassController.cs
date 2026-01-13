using ICR.Domain.Model.RepassAggregate;
using Microsoft.AspNetCore.Mvc;

namespace ICRManagement.API.Controllers
{
    [ApiController]
    [Route("api/v1/repasses")]
    public class RepassController : ControllerBase
    {
        private readonly IRepassRepository _repository;

        public RepassController(IRepassRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Repass model)
        {
            if (model == null) return BadRequest();
            _repository.Add(model);
            _repository.Save();
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, null);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var repass = _repository.GetById(id);
            if (repass == null) return NotFound();
            return Ok(repass);
        }

        [HttpGet]
        public IActionResult GetAll(int pageNumber = 1, int pageQuantity = 10)
        {
            var list = _repository.Get(pageNumber, pageQuantity);
            return Ok(list);
        }

        [HttpGet("by-church/{churchId}")]
        public IActionResult GetByChurch(long churchId)
        {
            var list = _repository.GetByChurchId(churchId);
            return Ok(list);
        }

        [HttpGet("by-reference/{reference}")]
        public IActionResult GetByReference(long reference)
        {
            var list = _repository.GetByReference(reference);
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
