using System.Reflection;

namespace Persistence.Data;

public class AppDbContext : DbContext
{
    //Data sets
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<EmailEntity> Emails => Set<EmailEntity>();

    //Construction
    public AppDbContext()
    {
    }

    //Lifecycle
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

}
