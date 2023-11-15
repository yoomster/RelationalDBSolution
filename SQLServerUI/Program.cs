﻿
using Microsoft.Extensions.Configuration;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using System.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        SqlCrud sql = new SqlCrud(GetConnectionString());

        //ReadAllContacts(sql);

        //ReadContact(sql, 3);

        //CreateNewContact(sql);

        //UpdateContact(sql);

        RemovePhoneNumberFromContact(sql, 3, 1004);

        Console.ReadLine();


    }

    private static void RemovePhoneNumberFromContact(SqlCrud sql, int contactId, int phoneNumberId)
    {
        sql.RemovePhoneNumberFromContact(contactId, phoneNumberId);
    }

    private static void UpdateContact(SqlCrud sql)
    {
        BasicContactModel contact = new BasicContactModel
        {
            Id = 1,
            FirstName = "Naomi",
            LastName = "Peren"
        };

        sql.UpdateContactName(contact);
    }

    private static void CreateNewContact(SqlCrud sql)
    {
        FullContactModel user = new FullContactModel
        {
            BasicInfo = new BasicContactModel
            {
                FirstName = "Adam",
                LastName = "Ak"
            },
        };

        user.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "adam@live.nl" });
        user.EmailAddresses.Add(new EmailAddressModel { Id = 2, EmailAddress = "me@naomi.nl" });


        user.PhoneNumbers.Add(new PhoneNumberModel { Id = 4, PhoneNumber = "0687654321" });
        user.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "049756246" });

        sql.CreateContact(user);
    }

    private static void ReadAllContacts(SqlCrud sql)
    {
        var rows = sql.GetAllContacts();

        foreach (var row in rows)
        {
            Console.WriteLine($"{row.Id}: {row.FirstName} {row.LastName}");
        }
    }

    private static void ReadContact(SqlCrud sql, int contactId)
    {
        var contact = sql.GetFullContactById(contactId);

        Console.WriteLine($"{contact.BasicInfo.Id}: {contact.BasicInfo.FirstName} {contact.BasicInfo.LastName}");
    }

    //unit testing could be great here: test if the connection string is returned
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