using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using Core.Entities;
using API.RequestHelpers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatePagedResult<T>(
    IGenericRepository<T> repo,
    ISpecification<T> spec,
    int pageIndex,
    int pageSize
) where T : BaseEntity
{
    var items = await repo.ListAsync(spec);
    var count = await repo.CountAsync(spec);

    var pagination = new Pagination<T>(pageIndex, pageSize, count, items);

    return Ok(pagination);
}

    }
}
