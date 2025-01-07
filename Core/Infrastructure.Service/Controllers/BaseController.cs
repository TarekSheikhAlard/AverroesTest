
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Application;
using Infrastructure.Domain.Models;
using Sieve.Models;

namespace Infrastructure.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<TAppService, TEntity,TGetDto,TCreatDto,TUpdateDto, TFilterDto>
        : ControllerBase,
        IBaseController<TAppService, TEntity, TGetDto, TCreatDto, TUpdateDto, TFilterDto>
        where TAppService : IBaseAppService<TGetDto, TCreatDto, TUpdateDto, TFilterDto>
        where TEntity : BasicEntity
        where TFilterDto : SieveModel
    {
        protected readonly TAppService _appService;

        public BaseController(TAppService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        public virtual async Task<ActionResult<TGetDto>> Create(TCreatDto dto)
        {
            var entity = await _appService.Create(dto);
            return Ok(entity);
        }

        [HttpPut]
        public virtual async Task<ActionResult<TGetDto>> Update(TUpdateDto dto)
        {
            return Ok(await _appService.Update(dto));
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TGetDto>> Get(int id)
        {
            
            var entity = await _appService.Get(id);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TGetDto>> Delete(int id)
        {
            var deletedEntity = await _appService.Delete(id);
            return Ok(deletedEntity);
        }
    }


}
