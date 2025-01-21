using Microsoft.AspNetCore.Mvc;
using vueproject_asp.Models;
using vueproject_asp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repository;

        public UserController(UserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _repository.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _repository.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var createdUser = await _repository.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.ID }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, User user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }

            var updatedUser = await _repository.UpdateUser(id, user);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var result = await _repository.DeleteUser(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
