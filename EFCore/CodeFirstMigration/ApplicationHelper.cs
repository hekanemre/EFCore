namespace CodeFirstMigration;

public class ApplicationHelper
{
    public static IConfigurationRoot Configuration;

    public static void Build() 
    {
        var directory = @"C:\Users\hakan\OneDrive\Masaüstü\Github\FirstRepo-EFCore\EFCore\EFCore\CodeFirstMigration";
        var builder = new ConfigurationBuilder().SetBasePath(directory/*Directory.GetCurrentDirectory()*/)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        Configuration = builder.Build();   
    }
}
