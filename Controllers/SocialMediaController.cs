using Microsoft.AspNetCore.Mvc;
using vueproject_asp.Models;
using vueproject_asp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly SocialMediaRepository _repository;

        // Constructor for SocialMediaController
        public SocialMediaController(SocialMediaRepository repository)
        {
            _repository = repository;
        }

        // HTTP GET: api/SocialMedia
        [HttpGet]
        public async Task<ActionResult<List<SocialMedia>>> GetSocialMedia()
        {
            var socialMedia = await _repository.GetSocialMedia();
            return Ok(socialMedia);
        }

        // HTTP GET: api/SocialMedia/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SocialMedia>> GetSocialMedia(int id)
        {
            // Convert the integer id to string before passing to repository
            var social = await _repository.GetSocialMediaById(id.ToString());

            if (social == null)
            {
                return NotFound();
            }

            return Ok(social);
        }

        // HTTP POST: api/SocialMedia
        [HttpPost]
        public async Task<ActionResult<SocialMedia>> CreateSocialMedia(SocialMedia socialMedia)
        {
            if (socialMedia == null)
            {
                return BadRequest();
            }

            var createdSocialMedia = await _repository.CreateSocialMedia(socialMedia);
            return CreatedAtAction(nameof(GetSocialMedia), new { id = createdSocialMedia.ID }, createdSocialMedia);
        }

        // HTTP PUT: api/SocialMedia/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSocialMedia(int id, SocialMedia socialMedia)
        {
            // Convert the integer id to string before passing to repository
            if (id.ToString() != socialMedia.ID)
            {
                return BadRequest();
            }

            var updatedSocialMedia = await _repository.UpdateSocialMedia(id.ToString(), socialMedia);

            if (updatedSocialMedia == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // HTTP DELETE: api/SocialMedia/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSocialMedia(int id)
        {
            // Convert the integer id to string before passing to repository
            var result = await _repository.DeleteSocialMedia(id.ToString());

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
