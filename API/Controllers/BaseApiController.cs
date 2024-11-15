using API.RequestHelper;
using Core.Contract.IGeneric;
using Core.Contract.SpecificationServices;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController:ControllerBase
    {
         protected async Task<ActionResult> CreatePageResult<T>(IGenericRepo<T> _repo,
          ISpecification<T> spec, 
          int pageIndex, int pageSize) where T : BaseEntity
         {
             var items = await _repo.ListAsync(spec);
             var count = await _repo.CountAsync(spec);
             var pagination = new PaginationHelper<T>(pageIndex,pageSize,count,items);
             return Ok(pagination);
         }     
    }
}