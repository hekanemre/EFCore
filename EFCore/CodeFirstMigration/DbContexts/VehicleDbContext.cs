namespace CodeFirstMigration.DbContexts;

public class VehicleDbContext : DbContext
{
    public DbSet<Brand> Brands { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ApplicationHelper.Build();
        optionsBuilder.UseSqlServer(ApplicationHelper.Configuration.GetConnectionString("SqlCon"));
    }
}
