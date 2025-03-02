namespace CodeFirstMigration.Services;

class DbSetMethodsService (
        VehicleDbContext context
    ): IDbSetMethodsService
{
    public async Task ManageMethods()
    {
        // FirstAsync() --> Retrieves the first element of the sequence, Throws an exception if no element is found
        var brand1 = await context.Brands.FirstAsync(x => x.Id == 1);
        // FirstOrDefaultAsync() --> Retrieves the first element of the sequence, Returns null (or default value for value types) if no element is found
        var brand2 = await context.Brands.FirstOrDefaultAsync(x => x.Id == 5);
        // SingleAsync() --> If there is not only one data like Id == 1 it returns error 
        var brand3 = await context.Brands.SingleAsync(x => x.Id == 1);
        // FindAsync() --> takes only primary key parameters. For this entity it is Id column
        var brand4 = await context.Brands.FindAsync(1);
        // Where() --> Filters a collection based on a condition and returns all matching elements
        // Returns an IQueryable<T> (query expression) that can be further modified before execution
        // Does not execute the query immediately—execution happens when you call methods like ToListAsync()
        var brand5 = await context.Brands.AsNoTracking().Where(x => x.Id > 0).ToListAsync(); //Additionally we can use AsNoTracking()
    }
}
