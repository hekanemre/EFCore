//This method is using for getting connection string from appsetting.json with
//Microsoft.Extension.FileExtension and Microsoft.Extension.Json nuget packages
ApplicationHelper.Build();

using (var context = new VehicleDbContext())
{
    var brands = await context.Brands.ToListAsync();

    brands.ForEach(x =>
    {
        Console.WriteLine($"{x.Id} : {x.Name}");
    });
}