using Microsoft.EntityFrameworkCore;
using TaskProject.Models.Domain;

namespace TaskProject.Data
{
    public class TaskDbContest : DbContext
    {
        public TaskDbContest(DbContextOptions options) : base(options)
        {}

        public DbSet<Models.Domain.Tasks> Tasks { get; set; }    
    }
}
