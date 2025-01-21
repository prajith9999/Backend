using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vueproject_asp.Models;
using vueproject_asp.Repositories;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturePageController : ControllerBase
    {
        private readonly FeaturePageRepository _repository;

        // Constructor for FeaturePageController
        public FeaturePageController(FeaturePageRepository repository)
        {
            _repository = repository;
        }

        // HTTP GET: api/FeaturePage
        [HttpGet]
        public async Task<ActionResult<List<FeaturePage>>> GetFeaturePages()
        {
            var featurePages = await _repository.GetFeaturePages();
            return Ok(featurePages);
        }

        // HTTP GET: api/FeaturePage/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FeaturePage>> GetFeaturePage(int id)
        {
            try
            {
                var featurePage = await _repository.GetFeaturePageById(id);
                return Ok(featurePage);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();  // Explicitly return NotFound if not found
            }
        }

        // HTTP POST: api/FeaturePage
        [HttpPost]
        public async Task<ActionResult<FeaturePage>> CreateFeaturePage(FeaturePage featurePage)
        {
            if (featurePage == null)
            {
                return BadRequest();  // Return BadRequest if invalid featurePage
            }

            var createdFeaturePage = await _repository.CreateFeaturePage(featurePage);
            return CreatedAtAction(nameof(GetFeaturePage), new { id = createdFeaturePage.ID }, createdFeaturePage);
        }

        // HTTP PUT: api/FeaturePage/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFeaturePage(int id, FeaturePage featurePage)
        {
            if (id != featurePage.ID)
            {
                return BadRequest();  // Return BadRequest if IDs do not match
            }

            try
            {
                await _repository.UpdateFeaturePage(featurePage); // Directly call repository without variable assignment
                return NoContent();  // Return NoContent (HTTP 204) if the update was successful
            }
            catch (KeyNotFoundException)
            {
                return NotFound();  // Return NotFound if the FeaturePage with the given ID is not found
            }
        }

        // HTTP DELETE: api/FeaturePage/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFeaturePage(int id)
        {
            var result = await _repository.DeleteFeaturePage(id);

            if (!result)
            {
                return NotFound();  // Return NotFound if the FeaturePage with the given ID is not found
            }

            return NoContent();  // Return NoContent if the deletion was successful
        }
    }
}
