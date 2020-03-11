using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sydvest_Bo.Models;
using Dapper;

namespace Sydvest_Bo
{
    public class DataAccess
    {

        //Returns all consultants that match the search text
        public List<Consultant> GetConsultants(string searchTxt)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
            {
                return conn.Query<Consultant>($"SELECT * FROM Consultants WHERE name LIKE '%{searchTxt}%'").ToList();
            }
        }

        public void InsertConsultant (Consultant consultant)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
            {
                string sql = "INSERT INTO Consultants ([name]) Values (@name);";

                var affectedRows = conn.Execute(sql, consultant);
                Console.WriteLine(affectedRows);
            }
        }
    }
}
