namespace CodeFirstMigration.DbContexts;

public class VehicleDbContext : DbContext
{
    public DbSet<Brand> Brands { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ApplicationHelper.Build();
        //optionsBuilder.UseSqlServer(Data Source=(localdb)\\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False);
        optionsBuilder.UseSqlServer(ApplicationHelper.Configuration.GetConnectionString("SqlCon"));
    }

    #region ChangeTracker on SaveChanges
        public override int SaveChanges()
        {
            //ChangeTracker is a propery of dbcontext that gives us entities that tracked in memory
            ChangeTracker.Entries().ToList().ForEach(p =>
            {
                //We inherit BaseEntity that entities which holds createddate attribute
                //So if we want to update any entity's created that inherits baseentity we can use:
                if (p.Entity is BaseEntity entity)
                {
                    if (p.State == EntityState.Added)
                    {
                        entity.CreatedDate = System.DateTime.Now;
                        entity.CreatedBy = 100;
                        entity.IsActive = true;
                    }
                }

                //If tracked data is a brand and it's state is added then make it's createddate datetime.now
                //if (p.Entity is Brand brand)
                //{
                //    if (p.State == EntityState.Added)
                //    {
                //        brand.CreatedDate = System.DateTime.Now;
                //    }
                //}
            });

            //After making related changes with creation we call SaveChanges method from base class
            return base.SaveChanges();
        }

    #endregion ChangeTracker on SaveChanges

}
