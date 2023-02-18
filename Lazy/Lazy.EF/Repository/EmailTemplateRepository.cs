using System.Linq.Expressions;
using Lazy.Data;
using Lazy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lazy.EF.Repository
{
    public class EmailTemplateRepository : RepositoryEF<EmailTemplate, int>
    {
        public EmailTemplateRepository(LazyContext context, ILogger<EmailTemplateRepository> logger)
            : base(logger, context)
        {
        }

        protected override DbSet<EmailTemplate> Set => Context.Set<EmailTemplate>();

        public override Task<PagedRepositoryResult<EmailTemplate>> ReadPagedAsync(int pageSize,
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

            return base.ReadPagedAsync(pageSize, pageNumber, filterExpression, sortExpression, projection1);
        }
    }
}