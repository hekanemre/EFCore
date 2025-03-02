namespace CodeFirstMigration.Services;

public class ChangeTrackerService (
        VehicleDbContext context
    ): IChangeTrackerService
{
    public async Task ManageTracker()
    {
        #region ContextId Id of an dbcontext instance
            Console.WriteLine($"Context instance Id of ChangeTrackerService is : {context.ContextId}");
        #endregion ContextId Id of an dbcontext instance

        #region AsNoTracking()
        //If we call any data with tolistasync all the data that we pull are tracking in memory
        //If we want to change data of course we need to track all data but if we want to read data we shouldn't track them in memory
        //We can use AsNoTracking method to make those data to not tracking in memory
        //var brands = context.Brands.AsNoTracking().ToListAsync();
        #endregion AsNoTracking()

        #region ChangeTracker attribute of DbContext
        var brands = await context.Brands.Where(x => x.IsActive).ToListAsync();
        if (!brands.Any(b => new[] { "FORD", "FIAT", "DODGE" }.Contains(b.Name.ToUpper())))
        {
            context.Add(new Brand() { Name = "FORD" });
            context.Add(new Brand() { Name = "FIAT" });
            context.Add(new Brand() { Name = "DODGE" });
            //Thanks to ChangeTracker if we have some fields that all inserted entities has (CreatedDate,CreatedBy) we can set values to these fields
            // This method only will change data that tracked in memory
            context.SaveChanges();
        }      
        #endregion ChangeTracker attribute of DbContext

    }
}
