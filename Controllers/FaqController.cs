using Microsoft.AspNetCore.Mvc;
using vueproject_asp.Models;
using vueproject_asp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : ControllerBase
    {
        private readonly FaqRepository _faqRepository;

        // Constructor for FaqController
        public FaqController(FaqRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }

        // HTTP GET: api/Faq
        [HttpGet]
        public async Task<ActionResult<List<Faq>>> GetFaqs()
        {
            var faqs = await _faqRepository.GetFaqs();
            if (faqs == null || faqs.Count == 0)
            {
                return NotFound();
            }
            return Ok(faqs);
        }

        // HTTP GET: api/Faq/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Faq>> GetFaqById(int id)
        {
            var faq = await _faqRepository.GetFaqById(id);
            if (faq == null)
            {
                return NotFound();
            }
            return Ok(faq);
        }

        // HTTP POST: api/Faq
        [HttpPost]
        public async Task<ActionResult<Faq>> CreateFaq(Faq faq)
        {
            if (faq == null)
            {
                return BadRequest("Invalid FAQ data.");
            }

            var createdFaq = await _faqRepository.CreateFaq(faq);
            return CreatedAtAction(nameof(GetFaqById), new { id = createdFaq.ID }, createdFaq);
        }

        // HTTP PUT: api/Faq/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFaq(int id, Faq faq)
        {
            if (id != faq.ID)
            {
                return BadRequest("FAQ ID mismatch.");
            }

            var updatedFaq = await _faqRepository.UpdateFaq(id, faq);
            if (updatedFaq == null)
            {
                return NotFound();
            }

            return NoContent(); // Successfully updated, no content to return
        }

        // HTTP DELETE: api/Faq/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFaq(int id)
        {
            var result = await _faqRepository.DeleteFaq(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent(); // Successfully deleted, no content to return
        }
    }
}
