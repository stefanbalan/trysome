using Lazy.Data.Entities;
using Lazy.Model;
using Lazy.Util.EntityModelMapper;

namespace Lazy.Server.Mappers;

public class EmailTemplatePagedResultMapper : PagedResultMapper<EmailTemplate, EmailTemplateModel>
{
    public EmailTemplatePagedResultMapper(IEntityModelMapper<EmailTemplate, EmailTemplateModel> mapper) : base(mapper)
    {
    }
}