using System.Linq.Expressions;
using Lazy.Client.Services;
using Lazy.Data;
using Lazy.Data.Entities;
using Lazy.Model;
using Lazy.Server.Infra;
using Lazy.Util.EntityModelMapper;
using Microsoft.AspNetCore.Mvc;

namespace Lazy.Server.Controllers;

[Route("api/[controller]")]
public class EmailTemplateController : ApiControllerWithPaging
{
    private readonly IRepository<EmailTemplate, int> _repository;
    private readonly IEntityModelMapper<EmailTemplate, EmailTemplateModel> _mapperEmailTemplate;

    private readonly IEntityModelMapper<PagedRepositoryResult<EmailTemplate>, PagedModelResult<EmailTemplateModel>>
        _mapperPagedResult;

    public EmailTemplateController(
        ILogger<EmailTemplateController> logger,
        UserSettingsService userSettingsService,
        IRepository<EmailTemplate, int> repository,
        IEntityModelMapper<EmailTemplate, EmailTemplateModel> mapperEmailTemplate,
        IEntityModelMapper<PagedRepositoryResult<EmailTemplate>, PagedModelResult<EmailTemplateModel>>
            mapperPagedResult)
        : base(logger, userSettingsService)
    {
        _repository = repository;
        _mapperEmailTemplate = mapperEmailTemplate;
        _mapperPagedResult = mapperPagedResult;
    }

    [HttpGet("", Name = "Paged list")]
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

        var repositoryResult = await _repository.ReadPagedAsync(ps, pn, filterExpression, null);
        var modelResult = _mapperPagedResult.GetModelFrom(repositoryResult);

        return Ok(modelResult);
    }

    // GET: api/Values/5
    [HttpGet("{id}", Name = "Get")]
    public async Task<ActionResult<EmailTemplateModel>> Get(int id)
    {
        try
        {
            var result = await _repository.ReadAsync(id);
            return result is null ? NotFound() : Ok(result);
        }
        catch (Exception e)
        {
            Logger.LogError("Error reading id {id}: {exception}", id, e.Message);
            return Problem(e.Message);
        }
    }

    // POST: api/Values
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] EmailTemplateModel emailTemplateModel)
    {
        // if(! ModelState.IsValid) //todo add validation (also clientside)
        //if( ! Validate<EmailTemplateModel> ( emailTemplateModel, out var validationResult ))
        //{
        //    return BadRequest(validationResult);
        //}

        try
        {
            var entity = _mapperEmailTemplate.GetEntityFrom(emailTemplateModel);
            var result = await _repository.CreateAsync(entity);
            var model = _mapperEmailTemplate.GetModelFrom(result);
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }
        catch (Exception e)
        {
            Logger.LogError("Error POST {model}: {exception}", emailTemplateModel, e.Message);
            return Problem(e.Message);
        }
    }

    // PUT: api/Values/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Put([FromBody] EmailTemplateModel emailTemplateModel)
    {
        if (string.IsNullOrWhiteSpace(emailTemplateModel.Name)) //todo add validation (fluent)
        {
            ModelState.AddModelError("Name", "The name should not be empty");
        }

        if (!ModelState.IsValid) 
            return BadRequest();

        try
        {
            var entity = _mapperEmailTemplate.GetEntityFrom(emailTemplateModel);
            if (entity.Id == 0)
            {
                var result = await _repository.CreateAsync(entity);
                var model = _mapperEmailTemplate.GetModelFrom(result);
                return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
            }
            else
            {
                var result = await _repository.UpdateAsync(entity);
                var model = _mapperEmailTemplate.GetModelFrom(result);
                return Ok(model);
            }
        }
        catch (Exception e)
        {
            Logger.LogError("Error PUT {model}: {exception}", emailTemplateModel, e.Message);
            return Problem(e.Message);
        }
    }

    // DELETE: api/Values/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _repository.DeleteAsync(new EmailTemplate() { Id = id });
            return NoContent();
        }
        catch (Exception e)
        {
            Logger.LogError("Error DEL {id}: {exception}", id, e.Message);
            return Problem(e.Message);
        }
    }
}
