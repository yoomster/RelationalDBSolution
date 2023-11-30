using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;



namespace MySqlUI
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlCrud sql = new MySqlCrud(GetConnectionString());

            //ReadAllContacts(sql);



            //CreateNewContact(sql);
            //ReadAllContacts(sql);

            //UpdateContact(sql);
            //ReadContact(sql, 2);

            RemovePhoneNumberFromContact(sql, 1, 1);
            ReadContact(sql, 1);


            Console.WriteLine("Done processing MySql");

            Console.ReadLine();

        }
        private static void RemovePhoneNumberFromContact(MySqlCrud sql, int contactId, int phoneNumberId)
        {
            sql.RemovePhoneNumberFromContact(contactId, phoneNumberId);
        }

        private static void UpdateContact(MySqlCrud sql)
        {
            BasicContactModel contact = new BasicContactModel
            {
                Id = 2,
                FirstName = "Adam",
                LastName = "Akii"
            };

            sql.UpdateContactName(contact);
        }

        private static void CreateNewContact(MySqlCrud sql)
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


            user.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "0612884703" });

            sql.CreateContact(user);
        }

        private static void ReadAllContacts(MySqlCrud sql)
        {
            var rows = sql.GetAllContacts();

            foreach (var row in rows)
            {
                Console.WriteLine($"{row.Id}: {row.FirstName} {row.LastName}");
            }
        }

        private static void ReadContact(MySqlCrud sql, int contactId)
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