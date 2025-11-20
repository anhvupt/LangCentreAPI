using LangCentre.Domain.Entities;
using LangCentre.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LangCentre.Infra.Persistent;

public class LangCentreDbContext(DbContextOptions<LangCentreDbContext> opts) : DbContext(opts)
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<ClassGroup> ClassGroups => Set<ClassGroup>();
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Enrolment> Enrolments => Set<Enrolment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Student>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<Instructor>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Course>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<ClassGroup>().HasKey(x => x.Id);
        modelBuilder.Entity<ClassGroup>()
            .HasMany(x => x.Classes)
            .WithOne(x => x.ClassGroup);

        modelBuilder.Entity<Class>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Class>()
            .HasOne(x => x.Instructor)
            .WithMany(x => x.Classes);
        
        modelBuilder.Entity<Enrolment>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Enrolment>()
            .HasOne(x => x.Student)
            .WithMany(x => x.Enrolments);
        modelBuilder.Entity<Enrolment>()
            .HasOne(x => x.Class)
            .WithMany(x => x.Enrolments);
    }
}