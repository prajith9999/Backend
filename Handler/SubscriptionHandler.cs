using System;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace LandWind.Handlers
{
    // Interface for SubscriptionHandler
    public interface ISubscriptionHandler
    {
        Task<IActionResult> GetSubscriptions();
        Task<IActionResult> GetSubscriptionById(int id);
        Task<IActionResult> CreateSubscription(Subscription subscription);
        Task<IActionResult> UpdateSubscription(int id, Subscription subscription);
        Task<IActionResult> DeleteSubscription(int id);
    }

    // SubscriptionHandler implementation
    public class SubscriptionHandler : ISubscriptionHandler
    {
        private readonly ISubscriptionRepository _repository;
        private readonly ILogger<SubscriptionHandler> _logger; // Add logger

        // Constructor for dependency injection
        public SubscriptionHandler(ISubscriptionRepository repository, ILogger<SubscriptionHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository)); // Null check for repository
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Null check for logger
        }

        // Get all subscriptions
        public async Task<IActionResult> GetSubscriptions()
        {
            try
            {
                var result = await _repository.GetAll();
                return result != null ? new OkObjectResult(result) : new NotFoundObjectResult("No subscriptions found."); // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching subscriptions");
                return HandleErrors(ex);
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
                _logger.LogError(ex, $"Error while fetching subscription with ID {id}");
                return HandleErrors(ex);
            }
        }

        // Create a new subscription
        public async Task<IActionResult> CreateSubscription(Subscription subscription)
        {
            if (subscription == null) return new BadRequestObjectResult("Invalid input."); // 400 Bad Request

            // Additional validation could be added here
            try
            {
                var result = await _repository.InsertSubscription(subscription);
                return new CreatedAtActionResult(nameof(GetSubscriptionById), "SubscriptionHandler", new { id = result }, result); // 201 Created
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating subscription");
                return HandleErrors(ex);
            }
        }

        public async Task<IActionResult> GetResultAsync(int id, Subscription subscription)
        {
            return (IActionResult)await _repository.UpdateSubscription(id, subscription);
        }

        // Update an existing subscription
        public async Task<IActionResult> UpdateSubscription(int id, Subscription subscription, object result)
        {
            if (id <= 0 || subscription == null) return new BadRequestObjectResult("Invalid input."); // 400 Bad Request

            try
            {
                return result == null ? new NotFoundObjectResult("Subscription not found.") : new OkObjectResult(result); // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating subscription with ID {id}");
                return HandleErrors(ex);
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
                _logger.LogError(ex, $"Error while deleting subscription with ID {id}");
                return HandleErrors(ex);
            }
        }

        // Centralized error handling for consistency
        private ObjectResult HandleErrors(Exception ex)
        {
            // You could introduce more specific exception classes if needed (e.g., NotFoundException)
            return new ObjectResult($"Error: {ex.Message}") { StatusCode = 500 }; // 500 Internal Server Error
        }

        public Task<IActionResult> UpdateSubscription(int id, Subscription subscription)
        {
            throw new NotImplementedException();
        }
    }
}
