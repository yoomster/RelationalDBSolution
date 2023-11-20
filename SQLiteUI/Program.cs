using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;



namespace SqliteUI
{
    class Program
    {
        static void Main(string[] args)
        {
            SqliteCrud sql = new SqliteCrud(GetConnectionString());

            //ReadAllContacts(sql);

            //ReadContact(sql, 13);

            //CreateNewContact(sql);

            //UpdateContact(sql);
            //ReadAllContacts(sql);


            RemovePhoneNumberFromContact(sql, 1, 1);


            Console.WriteLine("Done processing Sqlite");

            Console.ReadLine();

            //22.22

        }
        private static void RemovePhoneNumberFromContact(SqliteCrud sql, int contactId, int phoneNumberId)
        {
            sql.RemovePhoneNumberFromContact(contactId, phoneNumberId);
        }

        private static void UpdateContact(SqliteCrud sql)
        {
            BasicContactModel contact = new BasicContactModel
            {
                Id = 1,
                FirstName = "Nayoomi",
                LastName = "Peeer"
            };

            sql.UpdateContactName(contact);
        }

        private static void CreateNewContact(SqliteCrud sql)
        {
            FullContactModel user = new FullContactModel
            {
                BasicInfo = new BasicContactModel
                {
                    FirstName = "Baby",
                    LastName = "Akil"
                }
            };

            user.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "me@perenboom.nl" });
            //user.EmailAddresses.Add(new EmailAddressModel { Id = 10, EmailAddress = "me@adam.nl" });


            user.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "0612884703" });
            //user.PhoneNumbers.Add(new PhoneNumberModel { Id = 9, PhoneNumber = "04911111111" });

            sql.CreateContact(user);
        }

        private static void ReadAllContacts(SqliteCrud sql)
        {
            var rows = sql.GetAllContacts();

            foreach (var row in rows)
            {
                Console.WriteLine($"{row.Id}: {row.FirstName} {row.LastName}");
            }
        }

        private static void ReadContact(SqliteCrud sql, int contactId)
        {
            var contact = sql.GetFullContactById(contactId);

            Console.WriteLine($"{contact.BasicInfo.Id}: {contact.BasicInfo.FirstName} {contact.BasicInfo.LastName}");
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
}