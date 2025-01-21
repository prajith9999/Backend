using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vueproject_asp.Models;
using vueproject_asp.Repositories;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyController : ControllerBase
    {
        private readonly BodyRepository _bodyRepository;

        public BodyController(BodyRepository bodyRepository)
        {
            _bodyRepository = bodyRepository;
        }

        // GET: api/Body
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Body>>> GetBodies()
        {
            try
            {
                var bodies = await _bodyRepository.GetBodies();
                return Ok(bodies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Body/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Body>> GetBody(int id)
        {
            try
            {
                var body = await _bodyRepository.GetBodyById(id);

                if (body == null)
                {
                    return NotFound();
                }

                return Ok(body);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Body
        [HttpPost]
        public async Task<ActionResult<Body>> PostBody(Body body)
        {
            try
            {
                var createdBody = await _bodyRepository.InsertBody(body);
                return CreatedAtAction(nameof(GetBody), new { id = createdBody.ID }, createdBody);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Body/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Body>> PutBody(int id, Body body)
        {
            try
            {
                var updatedBody = await _bodyRepository.UpdateBody(id, body);

                if (updatedBody == null)
                {
                    return NotFound();
                }

                return Ok(updatedBody);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Body/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBody(int id)
        {
            try
            {
                var isDeleted = await _bodyRepository.DeleteBody(id);

                if (!isDeleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
