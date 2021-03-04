using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;
using Xuntos_testproject.Models;

namespace Xuntos_testproject.Database
{
    public class ApplicationDbContext
    {
        private readonly SqlCeConnection conn;
        public ApplicationDbContext()
        {
            conn = new SqlCeConnection(@"Data Source=|DataDirectory|\Umbraco.sdf");
            if (!TableExists("ProgrammingLanguage"))
            {
                CreateProgrammingLanguageTable();
            }
        }
        public IList<ProgrammingLanguage> GetProgrammingLanguages()
        {
            var SQL = "SELECT * FROM ProgrammingLanguage";
            var result = new List<ProgrammingLanguage>();
            conn.Open();
            SqlCeDataAdapter da = new SqlCeDataAdapter(SQL, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ProgrammingLanguage");
            foreach (var row in ds.Tables[0].Rows)
            {
                var drow = row as DataRow;
                ProgrammingLanguage language = new ProgrammingLanguage
                {
                    Id = Guid.Parse(drow["Id"].ToString()),
                    Name = drow["Name"].ToString(),
                    Experience = drow["Experience"].ToString()
                };
                result.Add(language);
            }
            conn.Close();
            return result;
        }

        public void PostProgrammingLanguage(ProgrammingLanguage language)
        {
            string SQL = "INSERT INTO ProgrammingLanguage(Id,Name,Experience) VALUES(@Id,@Name,@Experience)";
            SqlCeCommand cmdInsert = new SqlCeCommand(SQL, conn){ CommandType = CommandType.Text };
            cmdInsert.Parameters.AddWithValue("@Id", language.Id);
            cmdInsert.Parameters.AddWithValue("@Name", language.Name);
            cmdInsert.Parameters.AddWithValue("@Experience", language.Experience);
            conn.Open();
            cmdInsert.ExecuteNonQuery();
            conn.Close();
        }
        public void DeleteProgrammingLanguage(ProgrammingLanguage language)
        {
            string SQL = "DELETE FROM ProgrammingLanguage WHERE Id = @Id";
            SqlCeCommand cmdInsert = new SqlCeCommand(SQL, conn){CommandType = CommandType.Text};
            cmdInsert.Parameters.AddWithValue("@Id", language.Id);
            conn.Open();
            cmdInsert.ExecuteNonQuery();
            conn.Close();
        }

        private bool TableExists(string tableName)
        {
            using (var cmdExists = new SqlCeCommand())
            {
                var SQL = string.Format(
                        "SELECT COUNT(*) FROM information_schema.tables WHERE table_name = '{0}'",
                         tableName);
                cmdExists.Connection = conn;
                cmdExists.CommandText = SQL;
                conn.Open();
                var count = Convert.ToInt32(cmdExists.ExecuteScalar());
                conn.Close();
                return (count > 0);
            }
        }
        private void CreateProgrammingLanguageTable()
        {
            string SQL = "CREATE TABLE ProgrammingLanguage(Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), Name nvarchar(255), Experience nvarchar(255))";
            SqlCeCommand cmdCreate = new SqlCeCommand(SQL, conn);
            conn.Open();
            cmdCreate.ExecuteNonQuery();
            conn.Close();

            // Table entries 
            PostProgrammingLanguage(new ProgrammingLanguage("C#", "Used during Mediatechnology study and while building Xamarin Apps at previous job"));
            PostProgrammingLanguage(new ProgrammingLanguage("JavaScript", "Used for various applications with the Angular framework and seperate libraries"));
            PostProgrammingLanguage(new ProgrammingLanguage("PHP", "Used in back-end solutions with framework Symfony. Built API's with Doctrine ORM"));
            PostProgrammingLanguage(new ProgrammingLanguage("HTML/CSS", "Used for almost every web application made during my study and jobs"));
            PostProgrammingLanguage(new ProgrammingLanguage("Python/R", "Used for visualizations during graduation project"));
        }
    }
}