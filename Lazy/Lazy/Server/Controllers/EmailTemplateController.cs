using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Lazy.Data;
using Lazy.Data.Entities;
using Lazy.Model;


namespace Lazy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplateController : ControllerBase
    {
        private readonly IRepository<EmailTemplate, int> _repository;


        public EmailTemplateController(IRepository<EmailTemplate, int> repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public ActionResult<PagedResult<EmailTemplateModel>> Get(
            [FromQuery] int? pageSize,
            [FromQuery] int? pageNumber,
            [FromQuery] string? searchString)
        {
            var ps = pageSize ?? 10; //todo what defaults?
            var pn = pageNumber ?? 1; //todo sanitize?


            Expression<Func<EmailTemplate, bool>>? filterExpression =
                (!string.IsNullOrEmpty(searchString))
                    ? etm => etm.Name.Contains(searchString)
                    : null;

            var result = _repository.GetPaged(ps, pn, filterExpression, null);

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