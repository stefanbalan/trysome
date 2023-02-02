using Lazy.DB;
using Lazy.DB.Entities;
using Lazy.DB.EntityModelMapper;
using Lazy.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Lazy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplateController : ControllerBase
    {
        private readonly LazyContext _context;
        private readonly IEntityModelMapper<EmailTemplate, EmailTemplateModel> _emailTemplateMapper;

        public EmailTemplateController(LazyContext context, IEntityModelMapper<EmailTemplate, EmailTemplateModel> emailTemplateMapper)
        {
            _context = context;
            _emailTemplateMapper = emailTemplateMapper;
        }

        [HttpGet("")]
        public ActionResult<PagedResult<EmailTemplateModel>> Get(
            [FromQuery] int? pageSize,
            [FromQuery] int? pageNumber,
            [FromQuery] string? searchString)
        {
            var ps = pageSize ?? 10; //todo what defaults?
            var pn = pageNumber ?? 1; //todo sanitize?
            var query = _context.EmailTemplates as IQueryable<EmailTemplate>;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var ss = searchString.Replace(" ", "%");
                query = query.Where(et => et.Name.Contains(searchString));
            }

            var count = query.Count();
            var list = query.Skip((pn-1)*ps).Take(ps)
                .Select(et => _emailTemplateMapper.GetModelFrom(et))
                .ToList();

            var result = new PagedResult<EmailTemplateModel>()
            {
                PageSize = ps, 
                PageNumber = pn,
                SearchString = searchString ?? string.Empty,
                Results = list
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