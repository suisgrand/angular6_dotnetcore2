using System.Threading.Tasks;
using proj.IRepositories;
using proj.Persistence;

namespace proj.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskDbContext context;
        public UnitOfWork(TaskDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }

        // Task IUnitOfWork.CompleteAsync()
        // {
        //     throw new System.NotImplementedException();
        // }
    }
}