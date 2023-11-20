using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using Dapper;
using System.Data.SqlClient;

namespace DataAccessLibrary
{
    public class SqliteDataAccess
    {
        public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string sqlStatement, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameters);
            }
        }
    }
}


//		sqlStatement	"insert into PhoneNumbers(PhoneNumber) values(@PhoneNumber); "	string
//	DataAccessLibrary.dll!DataAccessLibrary.SqliteDataAccess.SaveData<<>f__AnonymousType3<string>>(string sqlStatement, <>f__AnonymousType3<string> parameters, string connectionString) Line 24	C#
// 	DataAccessLibrary.dll!DataAccessLibrary.SqliteCrud.CreateContact(DataAccessLibrary.Models.FullContactModel contact) Line 75	C#
// 	SQLiteUI.dll!SqliteUI.Program.CreateNewContact(DataAccessLibrary.SqliteCrud sql) Line 69	C#
// 	SQLiteUI.dll!SqliteUI.Program.Main(string[] args) Line 21	C#
//insert into PhoneNumbers(PhoneNumber) values(@PhoneNumber);



  //  <PackageReference Include="System.Data.SQLite.Core" Version="1.0.118" />
