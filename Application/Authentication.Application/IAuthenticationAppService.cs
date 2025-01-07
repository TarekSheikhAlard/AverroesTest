using Authentication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application
{
    public interface IAuthenticationAppService
    {
        public Task<AuthenticationModel> RegisterAsync(RegisterModel model);

        public Task<AuthenticationModel> GetTokenAsync(LogInModel model);

    }
}
