using System;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LandWind.Handlers
{
    public class PageContentHandler
    {
        private readonly IPageContentRepository _repository;

        public PageContentHandler(IPageContentRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IActionResult> GetPageContents()
        {
            try
            {
                var result = await _repository.GetPageContents();
                return result != null ? new OkObjectResult(result) : new NotFoundObjectResult("No page contents found.");  // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        public async Task<IActionResult> GetPageContentById(int id)
        {
            if (id <= 0) return new BadRequestObjectResult("Invalid ID provided.");  // 400 Bad Request

            try
            {
                var result = await _repository.GetPageContentById(id);
                return result == null ? new NotFoundObjectResult("PageContent not found.") : new OkObjectResult(result);  // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        public async Task<IActionResult> CreatePageContent(PageContent pageContent)
        {
            if (pageContent == null) return new BadRequestObjectResult("Invalid input.");  // 400 Bad Request

            try
            {
                var result = await _repository.CreatePageContent(pageContent);
                return new CreatedAtActionResult(nameof(GetPageContentById), new { id = result.ID }, result);  // 201 Created
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        public async Task<IActionResult> UpdatePageContent(int id, PageContent pageContent)
        {
            if (id <= 0 || pageContent == null) return new BadRequestObjectResult("Invalid input.");  // 400 Bad Request

            try
            {
                var result = await _repository.UpdatePageContent(pageContent);
                return result == null ? new NotFoundObjectResult("PageContent not found.") : new OkObjectResult(result);  // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        public async Task<IActionResult> DeletePageContent(int id)
        {
            if (id <= 0) return new BadRequestObjectResult("Invalid ID provided.");  // 400 Bad Request

            try
            {
                var result = await _repository.DeletePageContent(id);
                return result ? new OkResult() : new NotFoundObjectResult("PageContent not found.");  // 200 OK / 404 Not Found
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        private ObjectResult HandleError(Exception ex)
        {
            // Centralized error handling for consistency
            return new ObjectResult($"Error: {ex.Message}") { StatusCode = 500 };  // 500 Internal Server Error
        }
    }
}
