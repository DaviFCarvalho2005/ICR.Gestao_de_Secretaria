using ICR.Domain.Model.UserRoleAgreggate;
using Microsoft.AspNetCore.Mvc;

namespace ICRManagement.API.Controllers
{
    [ApiController]
    [Route("api/v1/userroles")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleAggregateRepository _repository;

        public UserRoleController(IUserRoleAggregateRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("user")]
        public IActionResult CreateUser([FromBody] User user)
        {
            _repository.AddUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, null);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetUser(long id)
        {
            var user = _repository.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return Ok(_repository.GetUsers());
        }

        [HttpDelete("user/{id}")]
        public IActionResult DeleteUser(long id)
        {
            _repository.DeleteUser(id);
            return NoContent();
        }

        [HttpPost("role")]
        public IActionResult CreateRole([FromBody] Role role)
        {
            _repository.AddRole(role);
            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, null);
        }

        [HttpGet("role/{id}")]
        public IActionResult GetRole(long id)
        {
            var role = _repository.GetRoleById(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            return Ok(_repository.GetRoles());
        }

        [HttpDelete("role/{id}")]
        public IActionResult DeleteRole(long id)
        {
            _repository.DeleteRole(id);
            return NoContent();
        }

        [HttpPost("user/{userId}/role/{roleId}")]
        public IActionResult AddUserRole(long userId, long roleId)
        {
            _repository.AddUserRole(userId, roleId);
            return NoContent();
        }

        [HttpDelete("user/{userId}/role/{roleId}")]
        public IActionResult RemoveUserRole(long userId, long roleId)
        {
            _repository.RemoveUserRole(userId, roleId);
            return NoContent();
        }

        [HttpGet("user/{userId}/roles")]
        public IActionResult GetRolesByUser(long userId)
        {
            return Ok(_repository.GetRolesByUserId(userId));
        }

        [HttpGet("role/{roleId}/users")]
        public IActionResult GetUsersByRole(long roleId)
        {
            return Ok(_repository.GetUsersByRoleId(roleId));
        }
    }
}
