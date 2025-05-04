using LandWind.Interfaces;
using LandWind.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Landwind.Handlers
{
    public class SubscriptionDetailsHandler
    {
        private readonly ISubscriptionDetailsRepository _repository;

        // Constructor for dependency injection
        public SubscriptionDetailsHandler(ISubscriptionDetailsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));  // Null check for repository
        }

        // Retrieve all subscription details
        public async Task<IActionResult> GetSubscriptionDetails()
        {
            try
            {
                var result = await _repository.GetSubscriptionDetails();
                return result != null ? new OkObjectResult(result) : new NotFoundObjectResult("No subscription details found.");  // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Retrieve subscription details by ID
        public async Task<IActionResult> GetSubscriptionDetailById(int id)
        {
            if (id <= 0) return new BadRequestObjectResult("Invalid ID provided.");  // 400 Bad Request

            try
            {
                var result = await _repository.GetSubscriptionDetailById(id);
                return result == null ? new NotFoundObjectResult("SubscriptionDetail not found.") : new OkObjectResult(result);  // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Create new subscription detail
        public async Task<IActionResult> CreateSubscriptionDetail(SubscriptionDetails subscriptionDetail)
        {
            if (subscriptionDetail == null) return new BadRequestObjectResult("Invalid input.");  // 400 Bad Request

            try
            {
                var result = await _repository.CreateSubscriptionDetail(subscriptionDetail);
                return new CreatedAtActionResult(nameof(GetSubscriptionDetailById), new { id = result.DetailID }, result);  // 201 Created
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Update subscription detail by ID
        public async Task<IActionResult> UpdateSubscriptionDetail(int id, SubscriptionDetails subscriptionDetail)
        {
            if (id <= 0 || subscriptionDetail == null) return new BadRequestObjectResult("Invalid input.");  // 400 Bad Request

            try
            {
                var result = await _repository.UpdateSubscriptionDetail(id, subscriptionDetail);
                return result == null ? new NotFoundObjectResult("SubscriptionDetail not found.") : new OkObjectResult(result);  // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Delete subscription detail by ID
        public async Task<IActionResult> DeleteSubscriptionDetail(int id, string deletedBy)
        {
            if (id <= 0 || string.IsNullOrEmpty(deletedBy)) return new BadRequestObjectResult("Invalid ID or missing 'deletedBy' value.");  // 400 Bad Request

            try
            {
                var result = await _repository.DeleteSubscriptionDetail(id, deletedBy);
                return result ? new OkResult() : new NotFoundObjectResult("SubscriptionDetail not found.");  // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Centralized error handling for consistency
        private ObjectResult HandleError(Exception ex)
        {
            return new ObjectResult($"Error: {ex.Message}") { StatusCode = 500 };  // 500 Internal Server Error
        }
    }
}
