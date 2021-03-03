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
        }

        public IList<ProgrammingLanguage> GetProgrammingLanguages()
        {
            var SQL = "SELECT * FROM ProgrammingLanguage";
            var result = new List<ProgrammingLanguage>();
            conn.Open();
            SqlCeDataAdapter da = new SqlCeDataAdapter(SQL, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ProgrammingLanguage");
            foreach(var row in ds.Tables[0].Rows)
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

        public void PostProgrammingLanguage (ProgrammingLanguage language)
        {
            string strCommand = "INSERT INTO ProgrammingLanguage(Id,Name,Experience) VALUES(@Id,@Name,@Experience)";
            conn.Open();
            SqlCeCommand cmdInsert = new SqlCeCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.CommandText = strCommand;
            cmdInsert.Parameters.AddWithValue("@Id", language.Id);
            cmdInsert.Parameters.AddWithValue("@Name", language.Name);
            cmdInsert.Parameters.AddWithValue("@Experience", language.Experience);
            cmdInsert.ExecuteNonQuery();
            conn.Close();
        }
    }
}