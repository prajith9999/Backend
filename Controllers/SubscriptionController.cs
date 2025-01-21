using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using vueproject_asp.Models;
using vueproject_asp.Repositories;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        // Primary constructor to inject the repository
        public SubscriptionController(SubscriptionRepository repository) =>
            _repository = repository;

        private readonly SubscriptionRepository _repository;

        // HTTP GET: api/Subscription
        [HttpGet]
        public async Task<ActionResult<List<Subscription>>> GetSubscription()
        {
            var subscriptions = await _repository.GetSubscription();
            return Ok(subscriptions);  // Use the built-in Ok method
        }

        // HTTP GET: api/Subscription/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscription>> GetSubscription(int id)
        {
            var subscription = await _repository.GetSubscriptionById(id);

            if (subscription == null)
            {
                return NotFound();  // Correctly return 404 if subscription is not found
            }

            return Ok(subscription);  // Use the built-in Ok method to return the subscription
        }

        // HTTP POST: api/Subscription
        [HttpPost]
        public async Task<ActionResult<Subscription>> CreateSubscription(Subscription subscription)
        {
            if (subscription == null) return BadRequest();  // Return 400 if subscription is null

            await _repository.InsertSubscription(subscription);
            return CreatedAtAction(nameof(GetSubscription), new { id = subscription.ID }, subscription);
        }

        // HTTP PUT: api/Subscription/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSubscription(int id, Subscription subscription)
        {
            if (id != subscription.ID) return BadRequest();  // Return 400 if IDs do not match

            await _repository.UpdateSubscription(subscription);
            return NoContent();  // Return 204 NoContent on successful update
        }

        // HTTP DELETE: api/Subscription/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubscription(int id)
        {
            var result = await _repository.DeleteSubscription(id);

            if (!result)
            {
                return NotFound();  // Return 404 if subscription is not found
            }

            return NoContent();  // Return 204 NoContent on successful deletion
        }
    }
}
