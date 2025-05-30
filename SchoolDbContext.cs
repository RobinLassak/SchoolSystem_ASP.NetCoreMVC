﻿using ASP.NetCoreMVC_SchoolSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCoreMVC_SchoolSystem
{
    public class SchoolDbContext : DbContext 
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options) { }
        public DbSet<Student> Students { get; set; }
    }
}
