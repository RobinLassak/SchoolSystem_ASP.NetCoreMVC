using ASP.NetCoreMVC_SchoolSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ASP.NetCoreMVC_SchoolSystem
{
    public class SchoolDbContext : IdentityDbContext<AppUsers> 
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }   
        public DbSet<Teacher> Teachers { get; set; }
    }
}
