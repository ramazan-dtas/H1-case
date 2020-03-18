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
    namespace DataAccess
    {
        public class Consultants
        {
            //Returns all consultants that match the search text
            public List<Consultant> Get (string searchTxt)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    return conn.Query<Consultant>($"SELECT * FROM Consultants WHERE name LIKE '%{searchTxt}%'").ToList();
                }
            }

            //Adds a new consultant to the Db
            public void Insert (Consultant consultant)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    string sql = "INSERT INTO Consultants ([name]) Values (@name);";

                    var affectedRows = conn.Execute(sql, consultant);
                    Console.WriteLine(affectedRows);
                }
            }

            //Updates an existing consultant in the Db
            public void Change (Consultant consultant)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    string sql = "UPDATE Consultants SET Name = (@name) WHERE id = (@id);";

                    var affectedRows = conn.Execute(sql, consultant);
                    Console.WriteLine(affectedRows);
                }
            }

            //Deletes an existing consultant from the Db
            public void Delete (Consultant consultant)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    string sql = "DELETE FROM Houses WHERE id = (@id);";

                    var affectedRows = conn.Execute(sql, consultant);
                    Console.WriteLine(affectedRows);
                }
            }
        }

        public class Houses
        {
            //Returns all houses that match the search text
            public List<House> Get (string searchTxt)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    return conn.Query<House>($"SELECT * FROM Houses WHERE name LIKE '%{searchTxt}%'").ToList();
                }
            }

            //Inserts a new house into the Db
            public void Insert (House house)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    string sql = "INSERT INTO Houses ([name, address ...]) Values (@name);";

                    var affectedRows = conn.Execute(sql, house);
                    Console.WriteLine(affectedRows);
                }
            }

            //Updates an existing house in the Db
            public void Change (House house)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    string sql = "UPDATE Houses SET ... = ... WHERE id = (@id);";

                    var affectedRows = conn.Execute(sql, house);
                    Console.WriteLine(affectedRows);
                }
            }

            //Deletes an existing house from the Db
            public void Delete (House house)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    string sql = "DELETE FROM Houses WHERE id = (@id);";

                    var affectedRows = conn.Execute(sql, house);
                    Console.WriteLine(affectedRows);
                }
            }
        }

        public class HouseOwners
        {
            //Returns all houseOwners that match the search text
            public List<HouseOwner> Get (string searchTxt)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    return conn.Query<HouseOwner>($"SELECT * FROM Houses WHERE name LIKE '%{searchTxt}%'").ToList();
                }
            }

            //Inserts a new houseOwner into the Db
            public void Insert (HouseOwner houseOwner)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    string sql = "INSERT INTO Houses ([name, address ...]) Values (@name);";

                    var affectedRows = conn.Execute(sql, houseOwner);
                    Console.WriteLine(affectedRows);
                }
            }

            //Updates an existing houseOwner in the Db
            public void Change (HouseOwner houseOwner)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    string sql = "UPDATE Houses SET ... = ... WHERE id = (@id);";

                    var affectedRows = conn.Execute(sql, houseOwner);
                    Console.WriteLine(affectedRows);
                }
            }

            //Deletes an existing houseOwner from the Db
            public void Delete (HouseOwner houseOwner)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    string sql = "DELETE FROM Houses WHERE id = (@id);";

                    var affectedRows = conn.Execute(sql, houseOwner);
                    Console.WriteLine(affectedRows);
                }
            }
        }

    }
}
