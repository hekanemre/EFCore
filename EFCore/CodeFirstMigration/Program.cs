//This method is using for getting connection string from appsetting.json with
//Microsoft.Extension.FileExtension and Microsoft.Extension.Json nuget packages
ApplicationHelper.Build();

using (var context = new VehicleDbContext())
{
    var brands = await context.Brands.ToListAsync();

    Console.WriteLine("State Changes");
    Console.WriteLine("------------READ-------------");
    brands.ForEach(x =>
    {
        var state = context.Entry(x).State;

        Console.WriteLine($"{x.Id} : {x.Name} --> state : {state}");
    });
    Console.WriteLine("------------READ-------------");
    Console.WriteLine("------------ADD-------------");
    var newBrand = new Brand()
    {
        Name = "DACIA"
    };

    Console.WriteLine($"Initial state of a new class that will be insert is --> {context.Entry(newBrand).State}");
    await context.AddAsync(newBrand);
    //Another alternative of an add operation is modifying state of a class with added
    //context.Entry(newBrand).State = EntityState.Added;
    Console.WriteLine($"State of a class that inserted is --> {context.Entry(newBrand).State}");
    //If we want to write our changes to the database we need to use SaveChange method
    await context.SaveChangesAsync();
    Console.WriteLine($"State after savechange is --> {context.Entry(newBrand).State}");
    Console.WriteLine("------------ADD-------------");
    Console.WriteLine("------------READ AND UPDATE-------------");
    var existingBrand = await context.Brands.FirstAsync();
    Console.WriteLine($"Initial state is --> {context.Entry(existingBrand).State}");
    existingBrand.Name = "ALFA ROMEO";
    Console.WriteLine($"Updated classes state is --> {context.Entry(existingBrand).State}");
    await context.SaveChangesAsync();
    Console.WriteLine($"State after savechange is --> {context.Entry(existingBrand).State}");
    Console.WriteLine("------------READ AND UPDATE-------------");
}