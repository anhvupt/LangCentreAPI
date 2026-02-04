using LangCentre.Domain.Entities;
using LangCentre.Domain.Events;
using LangCentre.Domain.Enums;
using LangCentre.Infra.Auditing;
using Microsoft.EntityFrameworkCore;

namespace LangCentre.Infra.Persistent;

public class LangCentreDbContext(
    DbContextOptions<LangCentreDbContext> opts,
    IDomainEventDispatcher domainEventDispatcher) : DbContext(opts)
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<ClassGroup> ClassGroups => Set<ClassGroup>();
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Enrolment> Enrolments => Set<Enrolment>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        var aggregateRoots = ChangeTracker.Entries()
            .Where(e => e.Entity is IAggregateRoot)
            .Select(e => (IAggregateRoot)e.Entity)
            .ToList();

        var allEvents = aggregateRoots.SelectMany(a => a.DomainEvents).ToList();

        if (allEvents.Count > 0)
        {
            await domainEventDispatcher.DispatchAsync(allEvents, cancellationToken);
            foreach (var root in aggregateRoots)
                root.ClearDomainEvents();

            result += await base.SaveChangesAsync(cancellationToken);
        }

        return result;
    }

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

        modelBuilder.Entity<AuditLog>().HasKey(x => x.Id);
    }
}