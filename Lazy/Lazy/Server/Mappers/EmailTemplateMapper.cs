using Lazy.DB.Entities;
using Lazy.DB.EntityModelMapper;
using Lazy.Shared;

namespace Lazy.Server.Mappers
{
    public sealed class EmailTemplateMapper : EntityModelMapperBase<EmailTemplate, EmailTemplateModel>
    {
        public override void BuildMappings()
        {
            MapTwoWay(entity => entity.Id, model => model.Id);
            MapTwoWay(entity => entity.Name, model => model.Name);
            MapTwoWay(entity => entity.Text, model => model.Text);
            MapTwoWay(entity => entity.Html, model => model.Html);
        }
    }
}