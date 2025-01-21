using Microsoft.AspNetCore.Mvc;
using vueproject_asp.Models;
using vueproject_asp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionDetailsController : ControllerBase
    {
        private readonly SubscriptionDetailsRepository _repository;

        // Constructor for SubscriptionDetailsController
        public SubscriptionDetailsController(SubscriptionDetailsRepository repository)
        {
            _repository = repository;
        }

        // HTTP GET: api/SubscriptionDetails
        [HttpGet]
        public async Task<ActionResult<List<SubscriptionDetails>>> GetSubscriptionDetails()
        {
            var subscriptionDetails = await _repository.GetSubscriptionDetails();
            return Ok(subscriptionDetails);
        }

        // HTTP GET: api/SubscriptionDetails/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionDetails>> GetSubscriptionDetail(int id)
        {
            var subscriptionDetail = await _repository.GetSubscriptionDetailById(id);

            if (subscriptionDetail == null)
            {
                return NotFound();
            }

            return Ok(subscriptionDetail);
        }

        // HTTP POST: api/SubscriptionDetails
        [HttpPost]
        public async Task<ActionResult<SubscriptionDetails>> CreateSubscriptionDetail(SubscriptionDetails subscriptionDetail)
        {
            if (subscriptionDetail == null)
            {
                return BadRequest();
            }

            var createdSubscriptionDetail = await _repository.CreateSubscriptionDetail(subscriptionDetail);
            return CreatedAtAction(nameof(GetSubscriptionDetail), new { id = createdSubscriptionDetail.ID }, createdSubscriptionDetail);
        }

        // HTTP PUT: api/SubscriptionDetails/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSubscriptionDetail(int id, SubscriptionDetails subscriptionDetail)
        {
            if (id != subscriptionDetail.ID)
            {
                return BadRequest();
            }

            var updatedSubscriptionDetail = await _repository.UpdateSubscriptionDetail(id, subscriptionDetail);

            if (updatedSubscriptionDetail == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // HTTP DELETE: api/SubscriptionDetails/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubscriptionDetail(int id)
        {
            var result = await _repository.DeleteSubscriptionDetail(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
