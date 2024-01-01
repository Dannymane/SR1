using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SR1.Model.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1.DAL.EF
{
   public class ApplicationDbContext : IdentityDbContext<User, Role, int>
   {
      public virtual DbSet<Group> Groups { get; set; }
      public virtual DbSet<Grade> Grades { get; set; }
      public virtual DbSet<Subject> Subjects { get; set; }
      public virtual DbSet<SubjectGroup> SubjectGroups { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);
         optionsBuilder.UseLazyLoadingProxies();
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);
         modelBuilder.Entity<User>()
                     .ToTable("AspNetUsers")
                     .HasDiscriminator<int>("UserType")
                     .HasValue<User>((int)RoleValue.User)
                     .HasValue<Student>((int)RoleValue.Student)
                     .HasValue<Parent>((int)RoleValue.Parent)
                     .HasValue<Teacher>((int)RoleValue.Teacher);

      }
   }
}
