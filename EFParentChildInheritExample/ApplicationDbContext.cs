using Microsoft.EntityFrameworkCore;

namespace EFParentChildInheritExample;

public class ApplicationDbContext : DbContext
{
    public DbSet<BaseClass> BaseClasses { get; set; }
    public DbSet<DerivedClassOne> DerivedClassOnes { get; set; }
    public DbSet<DerivedClassTwo> DerivedClassTwos { get; set; }
    public DbSet<DerivedClassThree> DerivedClassThrees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(x => x.MigrationsHistoryTable("MigrationHistory"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DerivedClassOne>()
            .HasMany(e => e.Children)
            .WithOne(e => (DerivedClassOne)e.ParentClass)
            .HasForeignKey(e => e.ParentId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<DerivedClassTwo>()
            .HasMany(e => e.Children)
            .WithOne(e => (DerivedClassTwo)e.ParentClass)
            .HasForeignKey(e => e.ParentId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<DerivedClassThree>()
            .HasOne(e => e.Child)
            .WithOne(e => (DerivedClassThree)e.ParentClass)
            .HasForeignKey<ChildClass>(e => e.ParentId)
            .HasPrincipalKey<DerivedClassThree>(e => e.Id);
    }
}
