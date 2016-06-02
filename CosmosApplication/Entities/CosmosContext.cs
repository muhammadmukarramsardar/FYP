using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.Entities
{
    public class CosmosContext : DbContext
    {
        public CosmosContext()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassSection> ClassSections { get; set; }
        public DbSet<Guardian> Guardians { get; set; }

        public DbSet<GuardianTime> GuardianTimes { get; set; }
        public DbSet<StudentGardians> StudentsGuardians { get; set; }
        public DbSet<StudentTime> StudentTimes { get; set; }

    }
}
