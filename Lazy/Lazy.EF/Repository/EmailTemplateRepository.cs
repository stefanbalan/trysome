using System.Linq.Expressions;
using Lazy.Data;
using Lazy.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lazy.EF.Repository
{
    public class EmailTemplateRepository : RepositoryEF<EmailTemplate, int>
    {
        public EmailTemplateRepository(LazyContext context)
            : base(context)
        {
        }

        protected override DbSet<EmailTemplate> Set => Context.Set<EmailTemplate>();

        public override Task<PagedRepositoryResult<EmailTemplate>> GetPagedAsync(int pageSize,
            int pageNumber,
            Expression<Func<EmailTemplate, bool>>? filterExpression,
            Expression<Func<EmailTemplate, bool>>? sortExpression,
            Expression<Func<EmailTemplate, EmailTemplate>>? projection = null)
        {
            var projection1 = projection ?? (et =>
                    new EmailTemplate
                    {
                        Id = et.Id,
                        Name = et.Name,
                        Html = et.Html,
                    });

            return base.GetPagedAsync(pageSize, pageNumber, filterExpression, sortExpression, projection1);
        }
    }
}