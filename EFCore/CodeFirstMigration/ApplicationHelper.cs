namespace CodeFirstMigration;

public class ApplicationHelper
{
    public static IConfigurationRoot Configuration;

    public static void Build() 
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var directory = Directory.GetParent(currentDirectory)?.Parent?.Parent?.FullName;
        var builder = new ConfigurationBuilder().SetBasePath(directory/*Directory.GetCurrentDirectory()*/)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        Configuration = builder.Build();   
    }
}
