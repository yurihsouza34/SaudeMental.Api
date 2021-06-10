using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaudeMental.Api.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync();
        Task<string> RegisterAsync(string userName);
        Task<string> GetTokenAsync(string userName);
        Task<string> GetIdAsync(string userName);
        Task<bool> DeleteAsync(string userName);
    }
}
