using System.Data.Entity;

namespace VSMSignalRPart2Demo.Models
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> People { get; set; }
    }
}