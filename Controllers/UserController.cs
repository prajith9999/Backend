using Microsoft.AspNetCore.Mvc;
using vueproject_asp.Data;
using vueproject_asp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using vueproject_asp.Repositories;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserRepository repository) : ControllerBase
    {
        private readonly UserRepository _repository = repository;

        // HTTP GET: api/User
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _repository.GetUsers();
            return Ok(users);
        }

        // HTTP GET: api/User/{id}
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

        // HTTP POST: api/User
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

        // HTTP PUT: api/User/{id}
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

        // HTTP DELETE: api/User/{id}
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
