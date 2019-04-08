using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using proj.IRepositories;
using proj.Models;
using proj.Resources;
using System;
using Microsoft.AspNetCore.Authorization;

namespace proj.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskUserController : Controller
    {
        private readonly ITaskUserRepository repository;
        public readonly IUnitOfWork uow;
        private readonly ILoginRepository rep_login;

        public TaskUserController(ITaskUserRepository repository, IUnitOfWork uow, ILoginRepository rep_login)
        {
            this.rep_login = rep_login;
            this.uow = uow;
            this.repository = repository;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<TaskUser>> GetTasks()
        {
            return await repository.GetTasks().ConfigureAwait(false);
        }

        [HttpGet("GetTasksForUser/{UserId}")]
        public async Task<IEnumerable<TaskUser>> GetTaskForUser(string UserId)
        {
            return await repository.GetTasksForUsers(UserId).ConfigureAwait(false);
        }

        [HttpGet("Get/{Id}")]
        public async Task<TaskUser> Get(int Id)
        {
            return await repository.Get(Id).ConfigureAwait(false);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            bool result = false;
            var status = string.Empty;

            try
            {
                await repository.Delete(id).ConfigureAwait(false);
                await uow.CompleteAsync().ConfigureAwait(false);
                result = true;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error On CreateNewCar controller endpoing. " + e.StackTrace);
                status = e.Message;
            }

            return Json(new { Success = result, Status = status });
        }

        [HttpPost("Add")]
        public async Task<IActionResult> CreateNewCar([FromBody] TaskUserResource taskUserResource)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = false;
            var status = string.Empty;

            try
            {
                var login = await rep_login.GetUser(taskUserResource.UserId).ConfigureAwait(false);
                var taskUser = new TaskUser
                {
                    Description = taskUserResource.Description,
                    LastUpdate = DateTime.Now,
                    LoginId = login.Id,
                    Check = taskUserResource.Check
                };
                await repository.Add(taskUser).ConfigureAwait(false);
                await uow.CompleteAsync().ConfigureAwait(false);
                result = true;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error On CreateNewCar controller endpoing. " + e.StackTrace);
                status = e.StackTrace;
            }
            return Json(new { Success = result, Status = status });

        }
    }
}