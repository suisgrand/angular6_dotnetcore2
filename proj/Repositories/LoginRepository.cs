using System.Threading.Tasks;
using proj.IRepositories;
using proj.Models;
using proj.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using proj.JWT;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security;

namespace proj.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly TaskDbContext context;
        private readonly AppSettings _appSettings;

        public LoginRepository(TaskDbContext context, IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
            this.context = context;
        }

        public void Create(Login login)
        {
            context.Add(login);
        }

        public async Task<IEnumerable<Login>> GetAllUsers()
        {
            return await context.Logins.ToListAsync().ConfigureAwait(false);
        }

        public async Task<bool> LoginUser(Login login)
        {
            var check = await context.Logins.AnyAsync(x => x.UserId.Equals(login.UserId, StringComparison.InvariantCultureIgnoreCase)
            && x.Password.Equals(x.Password)).ConfigureAwait(false);

            return check;
        }

        public Task LogoutUser(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Login> GetUser(string userId)
        {
            return await context.Logins.FirstOrDefaultAsync(x => x.UserId.Equals(userId, StringComparison.InvariantCultureIgnoreCase))
                .ConfigureAwait(false);
        }
    }
}