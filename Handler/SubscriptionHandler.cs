using LandWind.Interfaces;
using LandWind.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Landwind.Handlers
{
    public class SubscriptionHandler
    {
        private readonly ISubscriptionRepository _repository;

        // Constructor for dependency injection
        public SubscriptionHandler(ISubscriptionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));  // Null check for repository
        }

        // Get all subscriptions
        public async Task<IActionResult> GetSubscriptions()
        {
            try
            {
                var result = await _repository.GetSubscription();
                return result != null ? new OkObjectResult(result) : new NotFoundObjectResult("No subscriptions found."); // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Get subscription by ID
        public async Task<IActionResult> GetSubscriptionById(int id)
        {
            if (id <= 0) return new BadRequestObjectResult("Invalid ID provided."); // 400 Bad Request

            try
            {
                var result = await _repository.GetSubscriptionById(id);
                return result == null ? new NotFoundObjectResult("Subscription not found.") : new OkObjectResult(result); // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Create a new subscription
        public async Task<IActionResult> CreateSubscription(Subscription subscription)
        {
            if (subscription == null) return new BadRequestObjectResult("Invalid input."); // 400 Bad Request

            try
            {
                var result = await _repository.InsertSubscription(subscription);
                return new CreatedAtActionResult(nameof(GetSubscriptionById), new { id = result.ID }, result); // 201 Created
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Update an existing subscription
        public async Task<IActionResult> UpdateSubscription(int id, Subscription subscription)
        {
            if (id <= 0 || subscription == null) return new BadRequestObjectResult("Invalid input."); // 400 Bad Request

            try
            {
                var result = await _repository.UpdateSubscription(subscription);
                return result == null ? new NotFoundObjectResult("Subscription not found.") : new OkObjectResult(result); // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Delete subscription by ID
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            if (id <= 0) return new BadRequestObjectResult("Invalid ID provided."); // 400 Bad Request

            try
            {
                var result = await _repository.DeleteSubscription(id);
                return result ? new OkResult() : new NotFoundObjectResult("Subscription not found."); // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Centralized error handling for consistency
        private ObjectResult HandleError(Exception ex)
        {
            return new ObjectResult($"Error: {ex.Message}") { StatusCode = 500 }; // 500 Internal Server Error
        }
    }
}
