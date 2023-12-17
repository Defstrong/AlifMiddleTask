using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public sealed class AlifDbContext : DbContext
{
    public DbSet<DbClient> Clients => Set<DbClient>();
    public DbSet<DbWallet> Wallets => Set<DbWallet>();

    public AlifDbContext(DbContextOptions options) : base (options) { }
    public AlifDbContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}