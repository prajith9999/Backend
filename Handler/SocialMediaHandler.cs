using LandWind.Interfaces;
using LandWind.Models;
using Microsoft.AspNetCore.Mvc;

namespace LandWind.Handlers
{
    // Interface for SocialMediaHandler
    public interface ISocialMediaHandler
    {
        Task<IActionResult> GetSocialMedia();
        Task<IActionResult> GetSocialMediaById(int id);
        Task<IActionResult> CreateSocialMedia(SocialMedia socialMedia);
        Task<IActionResult> UpdateSocialMedia(int id, SocialMedia socialMedia);
        Task<IActionResult> DeleteSocialMedia(int id);
    }

    // SocialMediaHandler implementation
    public class SocialMediaHandler : ISocialMediaHandler
    {
        private readonly ISocialMediaRepository _repository;

        // Constructor to inject the ISocialMediaRepository
        public SocialMediaHandler(ISocialMediaRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));  // Null check for repository
        }

        // Get all social media
        public async Task<IActionResult> GetSocialMedia()
        {
            try
            {
                var result = await _repository.GetAll();
                return result != null && result.Count > 0 ? new OkObjectResult(result) : new NotFoundObjectResult("No social media found.");  // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Get social media by ID
        public async Task<IActionResult> GetSocialMediaById(int id)
        {
            if (id <= 0) return new BadRequestObjectResult("Invalid ID provided.");  // 400 Bad Request

            try
            {
                var result = await _repository.GetById(id);
                return result == null ? new NotFoundObjectResult("Social media not found.") : new OkObjectResult(result);  // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Create a new social media
        public async Task<IActionResult> CreateSocialMedia(SocialMedia socialMedia)
        {
            if (socialMedia == null)
            {
                return BadRequest("Invalid input.");  // 400 Bad Request
            }

            try
            {
                var result = await _repository.Create(socialMedia);

                // Ensure result is not null before proceeding
                if (result == null)
                {
                    return BadRequest("Failed to create social media.");  // 400 Bad Request
                }

                return CreatedAtAction(nameof(GetSocialMediaById), new { id = result.ID }, result);  // 201 Created
            }
            catch (Exception ex)
            {
                // Return a generic error handling response
                return HandleError(ex);
            }
        }

        private IActionResult CreatedAtAction(string v, object value, SocialMedia result)
        {
            throw new NotImplementedException();
        }

        private IActionResult BadRequest(string v)
        {
            throw new NotImplementedException();
        }


        // Update existing social media
        public async Task<IActionResult> UpdateSocialMedia(int id, SocialMedia socialMedia)
        {
            if (id <= 0 || socialMedia == null) return new BadRequestObjectResult("Invalid input.");  // 400 Bad Request

            try
            {
                var result = await _repository.Update(id, socialMedia);
                return result == null ? new NotFoundObjectResult("Social media not found.") : new OkObjectResult(result);  // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        // Delete social media by ID
        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            if (id <= 0) return new BadRequestObjectResult("Invalid ID provided.");  // 400 Bad Request

            try
            {
                var result = await _repository.Delete(id);
                return result ? new OkResult() : new NotFoundObjectResult("Social media not found.");  // 200 OK / 404 Not Found
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
