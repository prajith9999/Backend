using Microsoft.AspNetCore.Mvc;
using vueproject_asp.Models;
using vueproject_asp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooterController(FooterRepository repository) : ControllerBase
    {

        // HTTP GET: api/Footer
        [HttpGet]
        public async Task<ActionResult<List<Footer>>> GetFooters()
        {
            var footers = await repository.GetFooters();
            return Ok(footers);
        }

        // HTTP GET: api/Footer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Footer>> GetFooter(int id)
        {
            var footer = await repository.GetFooterById(id);

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

            var createdFooter = await repository.CreateFooter(footer);
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

            await repository.UpdateFooter(footer);
            return NoContent();
        }

        // HTTP DELETE: api/Footer/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFooter(int id)
        {
            var footer = await repository.GetFooterById(id);

            if (footer == null)
            {
                return NotFound();
            }

            await repository.DeleteFooter(id);
            return NoContent();
        }
    }
}
