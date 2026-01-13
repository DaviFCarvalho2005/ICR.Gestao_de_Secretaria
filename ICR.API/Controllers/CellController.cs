using ICR.Domain.DTOs;
using ICR.Domain.Model.CellAggregate;
using ICR.Domain.Model.MemberAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ICR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CellController : ControllerBase
    {
        private readonly ICellRepository _cellRepository;

        public CellController(ICellRepository cellRepository)
        {
            _cellRepository = cellRepository;
        }

        // GET api/cell?pageNumber=1&pageQuantity=10
        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageQuantity = 10)
        {
            var cells = await _cellRepository.GetAsync(pageNumber, pageQuantity);
            return Ok(cells);
        }

        // GET api/cell/5
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var cell = await _cellRepository.GetByIdAsync(id);

            if (cell == null)
                return NotFound();

            return Ok(cell);
        }

        // POST api/cell
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CellDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cell = new Cell(
                0,
                dto.Name,
                dto.ChurchId,
                dto.ResponsibleId ?? 0
            );

            await _cellRepository.AddAsync(cell);
            await _cellRepository.SaveAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = cell.Id },
                null
            );
        }

        // PATCH api/cell/5
        [HttpPatch("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromForm] CellDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedCell = new Cell(
                id,
                dto.Name,
                dto.ChurchId,
                dto.ResponsibleId ?? 0 // só pra satisfazer teu construtor torto
            );

            var updated = await _cellRepository.UpdateAsync(id, updatedCell);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE api/cell/5
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _cellRepository.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
