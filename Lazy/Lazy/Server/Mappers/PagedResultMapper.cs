using Lazy.Data;
using Lazy.Model;
using Lazy.Util.EntityModelMapper;

namespace Lazy.Server.Mappers;

public class PagedResultMapper<TEntity, TModel>
    : EntityModelMapper<PagedRepositoryResult<TEntity>, PagedModelResult<TModel>>
    where TEntity : new()
    where TModel : new()
{
    private readonly IEntityModelMapper<TEntity, TModel> _mapper;

    public PagedResultMapper(IEntityModelMapper<TEntity, TModel> mapper)
    {
        _mapper = mapper;

        MapEntityToModel(prr => prr.PageSize, pr => pr.PageSize);
        MapEntityToModel(prr => prr.PageNumber, pr => pr.PageNumber);
        MapEntityToModel(prr => prr.Count, pr => pr.Count);

        MapEntityToModel(prr => prr.Results.Select(e => _mapper.GetModelFrom(e)).ToList(), pr => pr.Results);
    }

}