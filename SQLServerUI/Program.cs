
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {

        Console.ReadLine();


    }

    //unit testing would be great here: test if the connection string is returned
    private static string GetConnectionString(string connectionStringName = "Default")
    {
        string output = "";

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        var config = builder.Build();

        output = config.GetConnectionString(connectionStringName);

        return output;
        }
}