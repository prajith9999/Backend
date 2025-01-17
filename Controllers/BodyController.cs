using Microsoft.AspNetCore.Mvc;
using vueproject_asp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using vueproject_asp.Repositories;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyController : ControllerBase
    {
        private readonly BodyRepository _repository;

        // Constructor to inject the BodyRepository
        public BodyController(BodyRepository repository)
        {
            _repository = repository;
        }

        // HTTP GET: api/Body
        [HttpGet]
        public async Task<ActionResult<List<Body>>> GetBodies()
        {
            var bodies = await _repository.GetBodies();
            return Ok(bodies);
        }

        // HTTP GET: api/Body/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Body>> GetBody(int id)
        {
            var body = await _repository.GetBodyById(id);

            if (body == null)
            {
                return NotFound();
            }

            return Ok(body);
        }

        // HTTP POST: api/Body
        [HttpPost]
        public async Task<ActionResult<Body>> CreateBody(Body body)
        {
            if (body == null)
            {
                return BadRequest("Body cannot be null.");
            }

            var createdBody = await _repository.InsertBody(body);
            return CreatedAtAction(nameof(GetBody), new { id = createdBody.ID }, createdBody);
        }

        // HTTP PUT: api/Body/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBody(int id, Body body)
        {
            if (id != body.ID)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                await _repository.UpdateBody(id, body);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Body not found.");
            }

            return NoContent();  // No content to return since the update was successful
        }

        // HTTP DELETE: api/Body/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBody(int id)
        {
            var result = await _repository.DeleteBody(id);

            if (!result)
            {
                return NotFound("Body not found.");
            }

            return NoContent();  // Successfully deleted, no content to return
        }
    }
}
