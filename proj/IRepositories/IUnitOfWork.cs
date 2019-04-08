using System.Threading.Tasks;

namespace proj.IRepositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}