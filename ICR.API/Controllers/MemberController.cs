using ICR.Application.Services;
using ICR.Application.ViewModel;
using ICR.Domain.DTOs;
using ICR.Domain.Model.FederationAggregate;
using ICR.Domain.Model.MemberAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ICRManagement.API.Controllers
{
    [ApiController]
    [Route("api/v1/members")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _repository;
        private readonly IdSequenceService _seq;


        public MemberController(IMemberRepository repository, IdSequenceService seq)
        {
            _repository = repository;
            _seq = seq;

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MemberDTO model)
        {

            var member = new Member(
                0,
                model.FamilyId,
                model.Name,
                model.Gender,
                model.BirthDate,
                model.HasBeenMarried,
                model.Role,
                model.CellPhone
            );
            
            await _repository.AddAsync(member);
            await _repository.SaveAsync();

            return Ok(new
            {
                member.FamilyId,
                member.Name,
                member.Gender,
                member.BirthDate,
                member.HasBeenMarried,
                member.Role,
                member.CellPhone
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var member = await _repository.GetByIdAsync(id);
            if (member == null) return NotFound();

            return Ok(member);
        }

        [HttpGet("family/{familyId}")]
        public async Task<IActionResult> GetByFamily(long familyId)
        {
            var members = await _repository.GetByFamilyAsync(familyId);
            return Ok(members);
        }

        [HttpGet("church/{churchId}")]
        public async Task<IActionResult> GetByChurch(long churchId)
        {
            var members = await _repository.GetByChurchAsync(churchId);
            return Ok(members);
        }

        [HttpGet("birthdays/{month}/{churchId}")]
        public async Task<IActionResult> GetBirthdays(int month, long churchId)
        {
            var members = await _repository.GetBirthdaysByMonthAsync(month, churchId);
            return Ok(members);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(long id, [FromForm] MemberDTO dto)
        {
            var member = await _repository.GetByIdAsync(id);
            if (member == null) return NotFound();

            member.SetFamily(dto.FamilyId);
            member.SetName(dto.Name);
            member.SetGender(dto.Gender);
            member.SetBirthDate(dto.BirthDate);
            member.SetRole(dto.Role);
            member.SetCellPhone(dto.CellPhone);

            _repository.UpdateAsync(member);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var member = await _repository.GetByIdAsync(id);
            if (member == null) return NotFound();

            _repository.RemoveAsync(member);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
