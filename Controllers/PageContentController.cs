using Microsoft.AspNetCore.Mvc;
using vueproject_asp.Models;
using vueproject_asp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageContentController : ControllerBase
    {
        private readonly PageContentRepository _repository;

        // Constructor for PageContentController
        public PageContentController(PageContentRepository repository)
        {
            _repository = repository;
        }

        // HTTP GET: api/PageContent
        [HttpGet]
        public async Task<ActionResult<List<PageContent>>> GetPageContents()
        {
            var pageContents = await _repository.GetPageContents();
            return Ok(pageContents);
        }

        // HTTP GET: api/PageContent/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PageContent>> GetPageContent(int id)
        {
            var pageContent = await _repository.GetPageContentById(id);

            if (pageContent == null)
            {
                return NotFound();
            }

            return Ok(pageContent);
        }

        // HTTP POST: api/PageContent
        [HttpPost]
        public async Task<ActionResult<PageContent>> CreatePageContent(PageContent pageContent)
        {
            if (pageContent == null)
            {
                return BadRequest("Page content cannot be null.");
            }

            var createdPageContent = await _repository.CreatePageContent(pageContent);
            return CreatedAtAction(nameof(GetPageContent), new { id = createdPageContent.ID }, createdPageContent);
        }

        // HTTP PUT: api/PageContent/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePageContent(int id, PageContent pageContent)
        {
            if (id != pageContent.ID)
            {
                return BadRequest("ID mismatch.");
            }

            await _repository.UpdatePageContent(pageContent);
            return NoContent();
        }

        // HTTP DELETE: api/PageContent/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePageContent(int id)
        {
            var result = await _repository.DeletePageContent(id);

            if (!result)
            {
                return NotFound("Page content not found.");
            }

            return NoContent();
        }
    }
}
