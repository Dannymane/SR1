using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using SR1.Model.DataModels;

namespace SR1.DAL.EF
{
   public class ApplicationDbContext: IdentityDbContext<User, Role, int>
   {
      public virtual DbSet<Grade> Grades { get; set; }
      public virtual DbSet<Group> Groups { get; set; }
      public virtual DbSet<Subject> Subjects { get; set; }
      public virtual DbSet<SubjectGroup> SubjectGroups { get; set; }

      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
      {

      }

      protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);
         optionsBuilder.UseLazyLoadingProxies();
      }

      protected void OnModelCreating(ModelBuilder modelBuilder)
      {
         //TBH - table per hierarchy:
         //So, the common IdentityUsers and our custom Users, Students.. will be stored in the same table
         // This table will have nullable columns related to all properties of User, Student.. + column UserType 
         // that specify the type of entity(row)
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
