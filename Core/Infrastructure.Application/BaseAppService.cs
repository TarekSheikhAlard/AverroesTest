using Infrastructure.Domain.Models;
using Sieve.Models;
using Sieve.Services;
using Infrastructure.Data.BaseDbContext;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Application.Exceptions;
using AutoMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using Infrastructure.Application.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Infrastructure.Application.BasicDto;

namespace Infrastructure.Application
{
    public abstract class BaseAppService<TServiceDbContext, TEntity, TGetDto, TCreateDto, TUpdateDto, TFilterDto> : IBaseAppService<TGetDto, TCreateDto, TUpdateDto, TFilterDto>
        where TServiceDbContext : BaseDbContext<TServiceDbContext>
        where TEntity : BasicEntity
        where TCreateDto : IValidatableDto
        where TUpdateDto : IEntityUpdateDto
        where TFilterDto : SieveModel
    {
        protected readonly TServiceDbContext _serviceDbContext;
        protected readonly ISieveProcessor _processor;
        protected readonly IMapper _mapper;
        protected AbstractValidator<IValidatableDto> _validations;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseAppService(TServiceDbContext serviceDbContext, IMapper mapper, ISieveProcessor processor, AbstractValidator<IValidatableDto> validations, IHttpContextAccessor httpContextAccessor)
        {
            _serviceDbContext = serviceDbContext;
            _processor = processor;
            _mapper = mapper;
            _validations = validations;
            _httpContextAccessor = httpContextAccessor;
        }
        protected virtual IQueryable<TEntity> QueryExcuter(TFilterDto input) => _serviceDbContext.Set<TEntity>().OrderByDescending(item => item.Id).AsQueryable();
        public virtual async Task<TGetDto> Delete(int id)
        {
            var result = await _serviceDbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? throw new EntityNotFoundException(typeof(TEntity).Name, id.ToString() ?? "");
            _serviceDbContext.Set<TEntity>().Remove(result);
            await _serviceDbContext.SaveChangesAsync();
            return await Task.FromResult(_mapper.Map<TGetDto>(result));
        }
        public virtual async Task<TGetDto> Get(int id)
        {
            
            var result = await QueryExcuter(null).FirstOrDefaultAsync(x => x.Id.Equals(id)) ??
            throw new EntityNotFoundException(typeof(TEntity).Name, id.ToString() ?? "");
            return await Task.FromResult(_mapper.Map<TGetDto>(result));
        }

        protected virtual TEntity BeforCreate(TCreateDto create)  
        {
            TEntity createdEntity = _mapper.Map<TEntity>(create);
            createdEntity.Code = typeof(TEntity).Name + "_" + Guid.NewGuid().ToString();
            if(createdEntity is BasicEntityWithAuditInfo)
            {
                (createdEntity as BasicEntityWithAuditInfo).CreatedBy = _httpContextAccessor.HttpContext?.User.FindFirst("userCode")?.Value;
                (createdEntity as BasicEntityWithAuditInfo).CreatedDate = DateTime.Now;
            }
            return createdEntity;
        }

        protected virtual TEntity BeforUpdate(TUpdateDto update, TEntity entity) => _mapper.Map(update, entity);


        public virtual async Task<TGetDto> Create(TCreateDto create)
        {
            var validationResult = await _validations.ValidateAsync(create, options => options.IncludeRuleSets("create", "default"));
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var entity = BeforCreate(create);
            var result = await _serviceDbContext.Set<TEntity>().AddAsync(entity);

            await _serviceDbContext.SaveChangesAsync();

            return await Get(result.Entity.Id);
        }
        public virtual async Task<TEntity> FindById(int id)
        {
            return await QueryExcuter(null).FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? throw new EntityNotFoundException(typeof(TEntity).Name, id.ToString() ?? "");

        }
        public virtual async Task<TGetDto> Update(TUpdateDto update)
        {
            var validationResult = await _validations.ValidateAsync(update, options => options.IncludeRuleSets("update", "default"));
            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var oldEntity = await FindById(update.Id);
            var newEntity = BeforUpdate(update, oldEntity);
            var result = _serviceDbContext.Set<TEntity>().Update(newEntity);
            await _serviceDbContext.SaveChangesAsync();
            return await Get(result.Entity.Id);
        }

        public virtual async Task<IEnumerable<TGetDto>> GetAll(TFilterDto input)
        {
            var result = await QueryExcuter(input).AsNoTracking().ToListAsync();
            var filterdResultForCount = _processor.Apply(input, result.AsQueryable(), applyPagination: false);
            var filterdResult = _processor.Apply(input, filterdResultForCount);
            return await Task.FromResult(new List<TGetDto>());
        }
    }
}
