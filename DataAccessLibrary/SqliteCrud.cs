﻿using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary
{
    public class SqliteCrud
    {
        private readonly string _connectionString;
        private SqliteDataAccess db = new();


        public SqliteCrud(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<BasicContactModel> GetAllContacts()
        {
            string sql = "select Id, FirstName, LastName from Contacts";

            return db.LoadData<BasicContactModel, dynamic>(sql, new { }, _connectionString);
        }

        public FullContactModel GetFullContactById(int id)
        {
            string sql = "select Id, FirstName, LastName from Contacts where Id = @Id";
            FullContactModel output = new()
            {
                BasicInfo = db.LoadData<BasicContactModel, dynamic>(sql, new { Id = id }, _connectionString).FirstOrDefault()
            };

            if (output.BasicInfo == null)
            {
                //throw new Exception("User not found.");
                return null;
            }

            sql = @"select e.*, ce.*
                    from EmailAddresses e
                    inner join ContactEmail ce on ce.EmailAddressId = e.Id
                    where ce.ContactID = @Id";

            output.EmailAddresses = db.LoadData<EmailAddressModel, dynamic>(sql, new { Id = id }, _connectionString);

            sql = @"select p.*
                    from PhoneNumbers p
                    inner join ContactPhone cp on cp.PhoneNumberId = p.Id
                    where cp.ContactID = @Id";

            output.PhoneNumbers = db.LoadData<PhoneNumberModel, dynamic>(sql, new { Id = id }, _connectionString);

            return output;
        }

        public void CreateContact(FullContactModel contact)
        {
            string sql = "insert into Contacts (FirstName, LastName) values (@FirstName, @LastName);";
            db.SaveData(sql,
                        new { contact.BasicInfo.FirstName, contact.BasicInfo.LastName },
                        _connectionString);

            sql = "select Id from Contacts where FirstName= @FirstName and LastName= @LastName;";
            int contactId = db.LoadData<IdLookUpModel, dynamic>(sql,
                new { contact.BasicInfo.FirstName, contact.BasicInfo.LastName },
                _connectionString).First().Id;

            foreach (var phoneNumber in contact.PhoneNumbers)
            {
                if (phoneNumber.Id == 0)
                {
                    sql = "insert into PhoneNumbers(PhoneNumber) values(@PhoneNumber); ";
                    db.SaveData(sql, new { phoneNumber.PhoneNumber }, _connectionString);

                    sql = "select Id from PhoneNumbers where PhoneNumber = @PhoneNumber;";
                    phoneNumber.Id = db.LoadData<IdLookUpModel, dynamic>
                        (sql, new { phoneNumber.PhoneNumber },
                        _connectionString).First().Id;
                }

                sql = "insert into ContactPhone(ContactID, PhoneNumberId) values(@ContactID, @PhoneNumberId); ";
                db.SaveData(sql, new { ContactID = contactId, PhoneNumberId = phoneNumber.Id }, _connectionString);
            }


            foreach (var email in contact.EmailAddresses)
            {
                if (email.Id == 0)
                {
                    sql = "insert into EmailAddresses(EmailAddress) values(@EmailAddress); ";
                    db.SaveData(sql, new { email.EmailAddress }, _connectionString);

                    sql = "select Id from EmailAddresses where EmailAddress = @EmailAddress;";
                    email.Id = db.LoadData<IdLookUpModel, dynamic>
                        (sql, new { email.EmailAddress },
                        _connectionString).First().Id;
                }

                sql = "insert into ContactEmail(ContactID, EmailAddressId) values(@ContactID, @EmailAddressId); ";
                db.SaveData(sql, new { ContactID = contactId, EmailAddressId = email.Id }, _connectionString);
            }

        }

        public void UpdateContactName(BasicContactModel contact)
        {
            string sql = "update Contacts set FirstName = @FirstName, LastName = @LastName where Id = @Id;";
            db.SaveData(sql, contact, _connectionString);
        }

        public void RemovePhoneNumberFromContact(int contactId, int phoneNumberId)
        {
            string sql = "select Id, ContactID,PhoneNumberId from ContactPhone where PhoneNumberId = @PhoneNumberId;";
            var links = db.LoadData<ContactPhoneNumberModel, dynamic>
                (sql, new { PhoneNumberId = phoneNumberId },
                _connectionString);

            sql = "delete from ContactPhone where PhoneNumberId = @PhoneNumberId and ContactId= @ContactId;";
            db.SaveData(sql, new { PhoneNumberId = phoneNumberId, ContactId = contactId },
                _connectionString);

            if (links.Count == 1)
            {
                sql = "delete from PhoneNumbers where Id = @PhoneNumberId ;";
                db.SaveData(sql, new { PhoneNumberId = phoneNumberId },
                _connectionString);
            }
        }
    }
}
