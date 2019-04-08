using System.Collections.Generic;
using System.Threading.Tasks;
using proj.Models;

namespace proj.IRepositories
{
    public interface ILoginRepository
    {
        Task<bool> LoginUser(Login login);
        Task LogoutUser(string userId);
        Task<Login> GetUser(string userId);

        void Create(Login login);
        Task<IEnumerable<Login>> GetAllUsers();
    }
}