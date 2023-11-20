using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        //SqlCrud sql = new SqlCrud(GetConnectionString());

        //ReadAllContacts(sql);

        //ReadContact(sql, 3);

        //CreateNewContact(sql);

        //UpdateContact(sql);

        //RemovePhoneNumberFromContact(sql, 3, 1004);

        Console.WriteLine("Done processing Sqlite");

        Console.ReadLine();


    }
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