using System;
using ts.Domain;
using ts.Domain.Entities;

namespace ts.Blazor.Server
{
    public class LocalizedSiteModel : ModelConvertibleBase<LocalizedSite, LocalizedSiteModel>
    {
        static LocalizedSiteModel()
        {
            Map(site => site.Id, model => model.Id);
            Map(site => site.Id, model => model.Id);
        }

        public int Id { get; set; }

        protected override void FromEntityToThisModel(LocalizedSite entity)
        {
            throw new NotImplementedException();
        }

        public override LocalizedSite ToEntity()
        {
            throw new NotImplementedException();
        }
    }

    public class Usage
    {
        public Usage()
        {
            var entity = new LocalizedSite();
            var site = LocalizedSiteModel.FromEntity(entity);
            entity = site.ToEntity();
        }
    }
}
