using System.Threading.Tasks;
using proj.IRepositories;
using proj.Models;
using proj.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace proj.Repositories
{
    public class TaskUserRepository : ITaskUserRepository
    {
        public TaskDbContext context { get; }
        public TaskUserRepository(TaskDbContext context)
        {
            this.context = context;

        }
        public async Task Add(TaskUser taskUser)
        {
            await context.TaskUsers.AddAsync(taskUser).ConfigureAwait(false);
        }

        public async Task Delete(int Id)
        {
            var taskUser = await Get(Id).ConfigureAwait(false);
            context.TaskUsers.Remove(taskUser);
        }

        public async Task<TaskUser> Get(int Id)
        {
            return await context.TaskUsers.FirstOrDefaultAsync(x => x.Id == Id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TaskUser>> GetTasks()
        {
            return await context.TaskUsers.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TaskUser>> GetTasksForUsers(string UserId)
        {
            return await context.TaskUsers
            .Include(x => x.Login)
            .Where(x => x.Login.UserId.Equals(UserId, StringComparison.InvariantCultureIgnoreCase))
            .ToListAsync().ConfigureAwait(false);
        }
    }
}