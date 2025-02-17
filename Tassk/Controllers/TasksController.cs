 using Microsoft.AspNetCore.Mvc;
using TaskProject.Data;
using TaskProject.Models;
using TaskProject.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TaskProject.Controllers
{
    [ApiController]
    [Route("Tasks")]
    public class TasksController : Controller
    {
        private readonly TaskDbContest taskDbContest;

        public TasksController(TaskDbContest taskDbContest) 
        {
            this.taskDbContest = taskDbContest;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Tasks>>> GetAll() 
        {
            var task = await taskDbContest.Tasks.ToListAsync();
            return (task);
        }

        [HttpGet("{Id}")]
        public async Task <ActionResult<Tasks>> GetByIndex(Guid Id)
        {
            var task = await taskDbContest.Tasks.Where(p => p.Id == Id).Select(p => new Tasks()
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Completed = p.Completed
            }).FirstOrDefaultAsync();
            if (task != null)
            {
                return task;
            }
            return NotFound();

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] UpdateTasks updateTaask)
        {
            var task = await taskDbContest.Tasks.FindAsync(Id);
            if (task != null)
            {
                task.Title = updateTaask.Title;
                task.Description = updateTaask.Description;
                task.Completed = updateTaask.Completed;
                taskDbContest.Tasks.Update(task);
                await taskDbContest.SaveChangesAsync();
                return NoContent();
            }
            return  NotFound();
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody]AddTasks addTaasks)
        {
            var task = new Tasks()
            {
                Id = Guid.NewGuid(),
                Title = addTaasks.Title,
                Description = addTaasks.Description,
                Completed = addTaasks.Completed,
            };
            await taskDbContest.Tasks.AddAsync(task);
            await taskDbContest.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) 
        {
            var task = await taskDbContest.Tasks.FindAsync(id);
            if (task!= null) 
            {
                taskDbContest.Remove(task);
                await taskDbContest.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }

    }
}
