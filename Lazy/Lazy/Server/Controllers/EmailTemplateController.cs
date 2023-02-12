using System.Linq.Expressions;
using Lazy.Client.Services;
using Lazy.Data;
using Lazy.Data.Entities;
using Lazy.Model;
using Lazy.Server.Infra;
using Lazy.Util.EntityModelMapper;
using Microsoft.AspNetCore.Mvc;


namespace Lazy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplateController : LazyPagingController
    {
        private readonly IRepository<EmailTemplate, int> _repository;

        private readonly IEntityModelMapper<PagedRepositoryResult<EmailTemplate>, PagedModelResult<EmailTemplateModel>>
            _mapper;


        public EmailTemplateController(
            UserSettingsService userSettingsService,
            IRepository<EmailTemplate, int> repository,
            IEntityModelMapper<PagedRepositoryResult<EmailTemplate>, PagedModelResult<EmailTemplateModel>> mapper)
            : base(userSettingsService)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<PagedModelResult<EmailTemplateModel>>> Get(
            [FromQuery] int? pageSize,
            [FromQuery] int? pageNumber,
            [FromQuery] string? searchString)
        {
            var (ps, pn) = ValidatePaging(pageSize, pageNumber);

            Expression<Func<EmailTemplate, bool>>? filterExpression =
                !string.IsNullOrEmpty(searchString)
                    ? etm => etm.Name.Contains(searchString)
                    : null;

            var repositoryResult = await _repository.GetPagedAsync(ps, pn, filterExpression, null);
            var modelResult = _mapper.GetModelFrom(repositoryResult);

            return Ok(modelResult);
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