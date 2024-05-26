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

         //    default DeleteBehaviors:
         //many-to-one: .OnDelete(DeleteBehavior.Cascade); 
         //one-to-one: .OnDelete(DeleteBehavior.ClientSetNull); 

         modelBuilder.Entity<Group>()
            .HasMany(g => g.Students)
            .WithOne(s => s.Group)
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.SetNull);
         //.IsRequired() unnecessary, the EF be default looks at type of property GroupId (int or int?)
         //But for strings 'string' is nullable be default, even without '?', use .IsRequired();

         modelBuilder.Entity<SubjectGroup>()
            .HasKey(sg => new { sg.SubjectId, sg.GroupId });

         modelBuilder.Entity<SubjectGroup>()
            .HasOne(sg => sg.Group)
            .WithMany(g => g.SubjectGroups)
            .HasForeignKey(sg => sg.GroupId);

         modelBuilder.Entity<SubjectGroup>()
            .HasOne(sg => sg.Subject)
            .WithMany(s => s.SubjectGroups)
            .HasForeignKey(sg => sg.SubjectId);

         modelBuilder.Entity<Group>()
            .HasMany(g => g.Students)
            .WithOne(s => s.Group)
            .HasForeignKey(s => s.GroupId) //GroupId implicitly bounded to `Id`
            //.HasPrincipalKey(s => s.Id2); //in case if Group has other PK that `Id`
            .OnDelete(DeleteBehavior.SetNull);

         /* DeleteBehavior.SetNull and DeleteBehavior.ClientSetNull
                 
            .SetNull creates a foreign key, that has `SET NULL` action - if the principal entity is deleted,
            the FK in the dependent entities are set to null.

            .ClientSetNull creates a foreign key without `SET NULL` action, but if you delete the principal entity
            in client (in code) - the FK in the dependent entities are set to null.

            Difference: if .ClientSetNull - removing the principal entity from database side doesn't affect 
            dependent entities.
         */

         modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Subjects)
            .WithOne(s => s.Teacher)
            .HasForeignKey(s => s.TeacherId)
            .OnDelete(DeleteBehavior.SetNull);
            

      }
   }
}
