using Microsoft.AspNetCore.Mvc;
using vueproject_asp.Models;
using vueproject_asp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly SubscriptionRepository _repository;

        // Constructor for SubscriptionController
        public SubscriptionController(SubscriptionRepository repository)
        {
            _repository = repository;
        }

        // HTTP GET: api/Subscription
        [HttpGet]
        public async Task<ActionResult<List<Subscription>>> GetSubscriptions()
        {
            var subscriptions = await _repository.GetSubscriptions();
            return Ok(subscriptions);
        }

        // HTTP GET: api/Subscription/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscription>> GetSubscription(int id)
        {
            var subscription = await _repository.GetSubscriptionById(id);

            if (subscription == null)
            {
                return NotFound();
            }

            return Ok(subscription);
        }

        // HTTP POST: api/Subscription
        [HttpPost]
        public async Task<ActionResult<Subscription>> CreateSubscription(Subscription subscription)
        {
            if (subscription == null)
            {
                return BadRequest();
            }

            // Create the subscription using Dapper
            await _repository.InsertSubscription(subscription);
            return CreatedAtAction(nameof(GetSubscription), new { id = subscription.ID }, subscription);
        }

        // HTTP PUT: api/Subscription/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSubscription(int id, Subscription subscription)
        {
            if (id != subscription.ID)
            {
                return BadRequest();
            }

            // Update the subscription using Dapper
            await _repository.UpdateSubscription(subscription);

            return NoContent();
        }

        // HTTP DELETE: api/Subscription/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubscription(int id)
        {
            var result = await _repository.DeleteSubscription(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
