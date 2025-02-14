using LandWind.Models;
using LandWind.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LandWind.Handlers
{
    // Interface for SubscriptionHandler
    public interface ISubscriptionHandlerDetail
    {
        Task<IActionResult> GetSubscriptions();
        Task<IActionResult> GetSubscriptionById(int id);
        Task<IActionResult> CreateSubscription(SubscriptionDetails subscription);
        Task<IActionResult> UpdateSubscription(int id, SubscriptionDetails subscription);
        Task<IActionResult> DeleteSubscription(int id);
    }

    // SubscriptionHandler implementation
    public class SubcriptionDetailsHandler : ISubscriptionHandlerDetail
    {
        private readonly ISubscriptionDetailsRepository _repository;

        // Constructor for dependency injection
        public SubcriptionDetailsHandler(ISubscriptionDetailsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));  // Null check for repository
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
                //return HandleError(ex);
                throw new Exception(ex.Message);
            }
        }

        // Get subscription by ID
        public async Task<IActionResult> GetSubscriptionById(int id)
        {
            if (id <= 0) return new BadRequestObjectResult("Invalid ID provided."); // 400 Bad Request

            try
            {
                var result = await _repository.GetById(id);
                return result == null ? new NotFoundObjectResult("Subscription not found.") : new OkObjectResult(result); // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Create a new subscription
        public async Task<IActionResult> CreateSubscription(SubscriptionDetails subscription)
        {
            if (subscription == null) return new BadRequestObjectResult("Invalid input."); // 400 Bad Request

            try
            {
                var result = await _repository.Create(subscription);
                return new CreatedAtActionResult(nameof(GetSubscriptionById), "GetSubscriptionById", new { id = result }, result); // 201 Created
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Update an existing subscription
        public async Task<IActionResult> UpdateSubscription(int id, SubscriptionDetails subscription)
        {
            if (id <= 0 || subscription == null) return new BadRequestObjectResult("Invalid input."); // 400 Bad Request

            try
            {
                var result = await _repository.Update(id, subscription); // Use ID in the update
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
                var result = await _repository.Delete(id);
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

        internal object GetResult()
        {
            throw new NotImplementedException();
        }
    }
}
