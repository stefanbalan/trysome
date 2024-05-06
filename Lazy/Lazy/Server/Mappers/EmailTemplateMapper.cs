using Lazy.Data.Entities;
using Lazy.Model;
using Lazy.Util.EntityModelMapper;

namespace Lazy.Server.Mappers;

public sealed class EmailTemplateMapper : EntityModelMapper<EmailTemplate, EmailTemplateModel>
{
    public EmailTemplateMapper()
    {
        MapTwoWay(entity => entity.Id, model => model.Id);
        MapTwoWay(entity => entity.Name, model => model.Name);
        MapTwoWay(entity => entity.Title, model => model.Title);
        MapTwoWay(entity => entity.Text, model => model.Text);
    }
}