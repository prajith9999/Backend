using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vueproject_asp.Models;
using vueproject_asp.Repositories;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly SocialMediaRepository _repository;

        // Constructor to inject the repository
        public SocialMediaController(SocialMediaRepository repository)
        {
            _repository = repository;
        }

        // HTTP GET: api/SocialMedia
        [HttpGet]
        public async Task<ActionResult<List<SocialMedia>>> GetSocialMedia()
        {
            var socialMediaList = await _repository.GetSocialMediaAsync();
            return Ok(socialMediaList);
        }

        // HTTP GET: api/SocialMedia/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SocialMedia>> GetSocialMediaById(int id)
        {
            var socialMedia = await _repository.GetSocialMediaByIdAsync(id);
            if (socialMedia == null)
            {
                return NotFound($"Social media with ID {id} not found.");
            }

            return Ok(socialMedia);
        }

        // HTTP POST: api/SocialMedia
        [HttpPost]
        public async Task<ActionResult<SocialMedia>> CreateSocialMedia(SocialMedia socialMedia)
        {
            if (socialMedia == null)
            {
                return BadRequest("Social media data cannot be null.");
            }

            // URL Validation
            if (string.IsNullOrEmpty(socialMedia.URL) || !Uri.IsWellFormedUriString(socialMedia.URL, UriKind.Absolute))
            {
                return BadRequest("Invalid URL format.");
            }

            var createdSocialMedia = await _repository.CreateSocialMediaAsync(socialMedia);
            return CreatedAtAction(nameof(GetSocialMediaById), new { id = createdSocialMedia.ID }, createdSocialMedia);
        }

        // HTTP PUT: api/SocialMedia/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSocialMedia(int id, SocialMedia socialMedia)
        {
            if (id != socialMedia.ID)
            {
                return BadRequest("ID mismatch.");
            }

            var updatedSocialMedia = await _repository.UpdateSocialMediaAsync(id, socialMedia);
            if (updatedSocialMedia == null)
            {
                return NotFound($"Social media with ID {id} not found.");
            }

            return NoContent();
        }

        // HTTP DELETE: api/SocialMedia/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSocialMedia(int id)
        {
            var result = await _repository.DeleteSocialMediaAsync(id);
            if (!result)
            {
                return NotFound($"Social media with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
