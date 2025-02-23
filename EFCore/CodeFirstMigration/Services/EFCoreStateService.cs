namespace CodeFirstMigration.Services;

public class EFCoreStateService (
        VehicleDbContext context
    ): IEFCoreStateService
{
    public async Task ManageStates()
    {
        #region ContextId Id of an dbcontext instance
            Console.WriteLine($"Context instance Id of EFCoreStateService is : {context.ContextId}");
        #endregion ContextId Id of an dbcontext instance

        #region READ --> STATE : UNCHANGED
        var brands = await context.Brands.ToListAsync();

            Console.WriteLine("State Changes");
            Console.WriteLine("------------READ-------------");
            brands.ForEach(x =>
            {
                var state = context.Entry(x).State;//UNCHANGED

                Console.WriteLine($"{x.Id} : {x.Name} --> state : {state}");
            });
            Console.WriteLine("------------READ-------------");
        #endregion READ --> STATE : UNCHANGED

        #region INSERT --> STATE : ADDED
            Console.WriteLine("------------ADD-------------");
            var newBrand = new Brand()
            {
                Name = "DACIA"
            };

            Console.WriteLine($"Initial state of a new class that will be insert is --> {context.Entry(newBrand).State}");//DETACHED
            await context.AddAsync(newBrand);
            //Another alternative of an add operation is modifying state of a class with added
            //context.Entry(newBrand).State = EntityState.Added;
            Console.WriteLine($"State of a class that inserted is --> {context.Entry(newBrand).State}");//ADDED
                                                                                                        //If we want to write our changes to the database we need to use SaveChange method
            await context.SaveChangesAsync();
            Console.WriteLine($"State after savechange is --> {context.Entry(newBrand).State}");//UNCHANGED
            Console.WriteLine("------------ADD-------------");
        #endregion INSERT --> STATE : ADDED

        #region UPDATE --> STATE : MODIFIED
            Console.WriteLine("------------READ AND UPDATE-------------");
            var existingBrand = await context.Brands.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            Console.WriteLine($"Initial state is --> {context.Entry(existingBrand).State}");//UNCHANGED
            existingBrand.Name = "ALFA ROMEO";
            Console.WriteLine($"Updated classes state is --> {context.Entry(existingBrand).State}");//MODIFIED

            //With using Update method we don't need to track any data we can give data manually
            //context.Update(new Brand() { Id = 1, Name = "VOLVO" });

            await context.SaveChangesAsync();
            Console.WriteLine($"State after savechange is --> {context.Entry(existingBrand).State}");//UNCHANGED
            Console.WriteLine("------------READ AND UPDATE-------------");
        #endregion UPDATE --> STATE : MODIFIED

        #region DELETE --> STATE : DELETED
            Console.WriteLine("------------READ AND DELETE-------------");
            var lastInsertedBrand = await context.Brands.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            Console.WriteLine($"Initial state is --> {context.Entry(lastInsertedBrand).State}");//UNCHANGED
            context.Remove(lastInsertedBrand);
            Console.WriteLine($"Removed classes state is --> {context.Entry(lastInsertedBrand).State}");//DELETED
            await context.SaveChangesAsync();
            Console.WriteLine($"State after savechange is --> {context.Entry(existingBrand).State}");//DETACHED
            Console.WriteLine("------------READ AND DELETE-------------");
        #endregion DELETE --> STATE : DELETED

    }
}
