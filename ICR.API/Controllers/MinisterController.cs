using ICR.Domain.Model.MinisterAggregate;
using Microsoft.AspNetCore.Mvc;

namespace ICRManagement.API.Controllers
{
    [ApiController]
    [Route("api/v1/ministers")]
    public class MinisterController : ControllerBase
    {
        private readonly IMinisterRepository _repository;

        public MinisterController(IMinisterRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Minister model)
        {
            if (model == null) return BadRequest();
            _repository.Add(model);
            _repository.Save();
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, null);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var minister = _repository.GetById(id);
            if (minister == null) return NotFound();
            return Ok(minister);
        }

        [HttpGet]
        public IActionResult GetAll(int pageNumber = 1, int pageQuantity = 10)
        {
            var ministers = _repository.Get(pageNumber, pageQuantity);
            return Ok(ministers);
        }

        [HttpGet("by-member/{memberId}")]
        public IActionResult GetByMember(long memberId)
        {
            var list = _repository.GetByMemberId(memberId);
            return Ok(list);
        }

        [HttpGet("by-family/{familyId}")]
        public IActionResult GetByFamily(long familyId)
        {
            var list = _repository.GetByFamilyId(familyId);
            return Ok(list);
        }

        [HttpGet("by-cpf/{cpf}")]
        public IActionResult GetByCpf(long cpf)
        {
            var m = _repository.GetByCpf(cpf);
            if (m == null) return NotFound();
            return Ok(m);
        }

        [HttpGet("by-church/{churchId}")]
        public IActionResult GetByChurch(long churchId)
        {
            var list = _repository.GetByChurchId(churchId);
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
