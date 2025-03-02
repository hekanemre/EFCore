//This method is using for getting connection string from appsetting.json with
//Microsoft.Extension.FileExtension and Microsoft.Extension.Json nuget packages
ApplicationHelper.Build();

// ServiceCollection is like a container that holds all the dependencies (services) your application needs
var services = new ServiceCollection();

// EF Core will create and dispose VehicleDbContext per request to avoid memory leaks
services.AddDbContext<VehicleDbContext>();
// Scoped: One instance per request (for web apps) or per scope. Best for DbContext.
// Whenever someone asks for IEFCoreStateService, give them an EFCoreStateService class
services.AddScoped<IEFCoreStateService, EFCoreStateService>();
services.AddScoped<IChangeTrackerService, ChangeTrackerService>();
services.AddScoped<IDbSetMethodsService, DbSetMethodsService>();

// This finalizes the DI container, making it ready for use
var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;

    // GetRequiredService<T>() retrieves an instance of IEFCoreStateService that was registered earlier
    // Since EFCoreStateService depends on VehicleDbContext, DI automatically injects it when EFCoreStateService is created
    var EFCoreStateService = scopedProvider.GetRequiredService<IEFCoreStateService>();
    await EFCoreStateService.ManageStates();

    var ChangeTrackerService = scopedProvider.GetRequiredService<IChangeTrackerService>();
    await ChangeTrackerService.ManageTracker();

    var DbSetMethodsService = scopedProvider.GetRequiredService<IDbSetMethodsService>();
    await DbSetMethodsService.ManageMethods();
}