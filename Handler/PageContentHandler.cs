using Microsoft.AspNetCore.Mvc;
using LandWind.Models;
using LandWind.Repositories;
using LandWind.Interfaces;

namespace LandWind.Handlers
{
    // Interface for PageContentHandler
    public interface IPageContentHandler
    {
        Task<List<PageContent>> GetAllPageContent();
        Task<IActionResult> GetPageContentById(int id);
        Task<IActionResult> CreatePageContent(PageContent pageContent);
        Task<IActionResult> UpdatePageContent(int id, PageContent pageContent);
        Task<IActionResult> DeletePageContent(int id);
        //Task<object?> GetPageContent();
        //Task<Body> InsertPageContent(PageContent pagecontent);
    }

    
    public class PageContentHandler : IPageContentHandler
    {
        private readonly IPageContentRepository _repository;

        public PageContentHandler(IPageContentRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IActionResult> GetPageContent()
        {
            try
            {
                var result = await _repository.GetPageContents();
                return result != null ? new OkObjectResult(result) : new NotFoundObjectResult("No page contents found.");
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        public async Task<IActionResult> GetPageContentById(int id)
        {
            if (id <= 0) return new BadRequestObjectResult("Invalid ID provided.");

            try
            {
                var result = await _repository.GetPageContentById(id);
                return result == null ? new NotFoundObjectResult("PageContent not found.") : new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        public async Task<IActionResult> CreatePageContent(PageContent pageContent)
        {
            if (pageContent == null) return new BadRequestObjectResult("Invalid input.");

            try
            {
                var result = await _repository.CreatePageContent(pageContent);
                return new CreatedResult(string.Empty, result);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        public async Task<IActionResult> UpdatePageContent(int id, PageContent pageContent)
        {
            if (id <= 0 || pageContent == null) return new BadRequestObjectResult("Invalid input.");

            try
            {
                var result = await _repository.UpdatePageContent(id, pageContent);
                return result == null ? new NotFoundObjectResult("PageContent not found.") : new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        public async Task<IActionResult> DeletePageContent(int id)
        {
            if (id <= 0) return new BadRequestObjectResult("Invalid ID provided.");

            try
            {
                var result = await _repository.DeletePageContent(id);
                return result ? new OkResult() : new NotFoundObjectResult("PageContent not found.");
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        private ObjectResult HandleError(Exception ex)
        {
            return new ObjectResult($"Error: {ex.Message}") { StatusCode = 500 };
        }

        public async Task<List<PageContent>> GetAllPageContent()
        {
            try
            {
                return await _repository.GetPageContents();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
               
            }
        }

       
        //public async Task<Body> InsertPageContent(PageContent pagecontent)
        //{
        //    if (pagecontent == null) throw new ArgumentNullException(nameof(pagecontent), "Body cannot be null.");

        //    try
        //    {
        //        return await _repository.InsertBody(body);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while inserting the body.");
        //        throw new InvalidOperationException("An error occurred while inserting the body.", ex);
        //    }
        //}
    
        }
    }

