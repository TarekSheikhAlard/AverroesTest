using Infrastructure.Data.BaseDbContext;
using Infrastructure.Domain.Models;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Application
{
    public interface IBaseAppService<TGetDto, TCreatDto, TUpdateDto,TFilterDto>
    where TFilterDto : SieveModel
    {
        public Task<TGetDto> Create(TCreatDto dto);
        public Task<TGetDto> Delete(int id);
        public Task<TGetDto> Get(int id);
        public Task<TGetDto> Update(TUpdateDto update);
        public Task<IEnumerable<TGetDto>> GetAll(TFilterDto input);
    }
}
