//This method is using for getting connection string from appsetting.json with
//Microsoft.Extension.FileExtension and Microsoft.Extension.Json nuget packages
ApplicationHelper.Build();

var services = new ServiceCollection();

// Register DbContext and the service
services.AddDbContext<VehicleDbContext>();
services.AddScoped<IEFCoreStateService, EFCoreStateService>();

var serviceProvider = services.BuildServiceProvider();

// Resolve and execute service
var eFCoreStateService = serviceProvider.GetRequiredService<IEFCoreStateService>();
await eFCoreStateService.ManageStates();