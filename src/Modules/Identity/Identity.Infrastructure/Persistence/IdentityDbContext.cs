using Identity.Domain.Entity;


namespace Identity.Infrastructure.Persistence;

public class IdentityDbContext: DbContext,IIdentityDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }



    public IdentityDbContext(DbContextOptions<IdentityDbContext> options): base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);


        modelBuilder.HasDefaultSchema("identity");
        modelBuilder.Entity<User>().HasQueryFilter(u => u.IsActive);

        base.OnModelCreating(modelBuilder);
    }
}
