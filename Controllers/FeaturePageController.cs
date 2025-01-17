using Microsoft.AspNetCore.Mvc;
using vueproject_asp.Models;
using vueproject_asp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var featurePage = await _repository.GetFeaturePageById(id);

            if (featurePage == null)
            {
                return NotFound();
            }

            return Ok(featurePage);
        }

        // HTTP POST: api/FeaturePage
        [HttpPost]
        public async Task<ActionResult<FeaturePage>> CreateFeaturePage(FeaturePage featurePage)
        {
            if (featurePage == null)
            {
                return BadRequest();
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
                return BadRequest();
            }

            await _repository.UpdateFeaturePage(featurePage);  // No need to assign a variable here

            return NoContent();
        }

        // HTTP DELETE: api/FeaturePage/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFeaturePage(int id)
        {
            var result = await _repository.DeleteFeaturePage(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
