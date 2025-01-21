using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vueproject_asp.Models;
using vueproject_asp.Repositories;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooterController : ControllerBase
    {
        private readonly FooterRepository _repository;

        // Constructor for FooterController
        public FooterController(FooterRepository repository)
        {
            _repository = repository;
        }

        // HTTP GET: api/Footer
        [HttpGet]
        public async Task<ActionResult<List<Footer>>> GetFooters()
        {
            var footer = await _repository.GetFooter();
            return Ok(footer);
        }

        // HTTP GET: api/Footer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Footer>> GetFooter(int id)
        {
            var footer = await _repository.GetFooterById(id);

            if (footer == null)
            {
                return NotFound();
            }

            return Ok(footer);
        }

        // HTTP POST: api/Footer
        [HttpPost]
        public async Task<ActionResult<Footer>> CreateFooter(Footer footer)
        {
            if (footer == null)
            {
                return BadRequest();
            }

            var createdFooter = await _repository.CreateFooter(footer);
            return CreatedAtAction(nameof(GetFooter), new { id = createdFooter.ID }, createdFooter);
        }

        // HTTP PUT: api/Footer/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFooter(int id, Footer footer)
        {
            if (id != footer.ID)
            {
                return BadRequest();
            }

            await _repository.UpdateFooter(footer);
            return NoContent();
        }

        // HTTP DELETE: api/Footer/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFooter(int id)
        {
            var footer = await _repository.GetFooterById(id);

            if (footer == null)
            {
                return NotFound();
            }

            await _repository.DeleteFooter(id);
            return NoContent();
        }
    }
}
