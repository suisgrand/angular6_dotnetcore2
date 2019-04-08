using System.Collections.Generic;
using System.Threading.Tasks;
using proj.Models;

namespace proj.IRepositories
{
    public interface ITaskUserRepository
    {
        Task Add(TaskUser task);
        Task<TaskUser> Get(int Id);
        Task Delete(int Id);
        Task<IEnumerable<TaskUser>> GetTasks();
        Task<IEnumerable<TaskUser>> GetTasksForUsers(string UserId);
    }
}