using Lazy.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Lazy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplateController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult<PagedResult<EmailTemplateModel>> Get([FromQuery] int? pageSize, [FromQuery] int? pageNumber,
            [FromQuery] string? searchString)
        {
            var r = new List<EmailTemplateModel>()
            {
                new() { Id = 1, Name = "Template 1" },
                new() { Id = 2, Name = "Template 2" },
                new() { Id = 3, Name = "Template 3" },
                new() { Id = 4, Name = "Template 4" },
                new() { Id = 5, Name = "Template 5" },
                new() { Id = 6, Name = "Template 6" },
                new() { Id = 7, Name = "Template 7" },
                new() { Id = 8, Name = "Template 8" },
                new() { Id = 9, Name = "Template 9" },
                new() { Id = 10, Name = "Template 10" },
                new() { Id = 11, Name = "Template 11" },
            };
            var result = new PagedResult<EmailTemplateModel>()
            {
                PageSize = pageSize ?? 10, //todo what defaults?
                PageNumber = pageNumber ?? 1,
                SearchString = searchString ?? string.Empty,
                Results = r
            };
            return Ok(result);
        }

        // GET: api/Values/5
        [HttpGet("{id}", Name = "Get")]
        public EmailTemplateModel Get(int id)
        {
            return new EmailTemplateModel()
            {
                Id = 1,
                Text =
                    @"An <em>unhandled</em> error has occurred.",
                Html = true
            };
        }

        // POST: api/Values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] EmailTemplateModel emailTemplate)
        {
            //if (check)
            //{
            //    return BadRequest();
            //}

            // _context.Email.Add(emailTemplate);
            // await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = emailTemplate.Id }, emailTemplate);
        }

        // PUT: api/Values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}