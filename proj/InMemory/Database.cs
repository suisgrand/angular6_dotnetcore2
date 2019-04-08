using System.Threading.Tasks;
using proj.IRepositories;
using proj.Models;
using proj.Persistence;

namespace proj.InMemory
{
    public class Database
    {
        private readonly TaskDbContext context;

        public Database(TaskDbContext context)
        {
            this.context = context;

        }



        public void PopulateDefaultValues()
        {
            var login = new Login
            {
                Id = 0,
                UserId = "test",
                Password = "pwd123"
            };

            context.Logins.Add(login);
            context.SaveChanges();
        }
    }
}