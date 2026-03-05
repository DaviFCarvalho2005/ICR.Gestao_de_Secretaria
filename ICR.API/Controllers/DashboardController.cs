using ICR.Domain.DTOs;
using ICR.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace ICR.API.Controllers
{
    [ApiController]
    [Route("api/v1/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _repository;

        public DashboardController(IDashboardRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("stats")]
        public async Task<ActionResult<DashboardStatsDTO>> GetStats()
        {
            var stats = await _repository.GetTotalStatsAsync();
            return Ok(stats);
        }
    }
}