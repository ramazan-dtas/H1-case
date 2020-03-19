using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sydvest_Bo.Models;
using Dapper;
using System.Reflection;

namespace Sydvest_Bo
{
    namespace DataAccess
    {
        public class Consultants
        {
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
                    
                }
            }
        }

        public class HouseOwners
        {
            //Inserts a new houseOwner into the Db
            public void Insert (HouseOwner houseOwner)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    string sql = "INSERT INTO HouseOwners ([name]) Values (@name);";

                    var affectedRows = conn.Execute(sql, houseOwner);
                    Console.WriteLine($"New Owner '{houseOwner.name}' added!");
                    Console.ReadKey();
                }
            }

            //Updates an existing houseOwner in the Db
            public void Change (HouseOwner houseOwner)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    string sql = "UPDATE HouseOwners SET name = (@name) WHERE id = (@id);";

                    try
                    {
                        var affectedRows = conn.Execute(sql, houseOwner);
                        Console.WriteLine($"Owner {houseOwner.id} updated!");
                        Console.ReadKey();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                    }
                }
            }

            //Deletes an existing houseOwner from the Db
            public void Delete (HouseOwner houseOwner)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
                {
                    string sql = "DELETE FROM HouseOwners WHERE id = (@id);";

                    try
                    {
                        var affectedRows = conn.Execute(sql, houseOwner);
                        Console.WriteLine($"Owner {houseOwner.name} ({houseOwner.id}) deleted!");
                        Console.ReadKey();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                    }
                }
            }
        }

    }

    public class DbAccess
    {
        //Searches through a table and returns a list with all matched entries
        public List<object> GetList (object obj, string tableName, string searchTxt)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
            {
                string sql = $"SELECT * FROM {tableName} WHERE ";
                PropertyInfo[] props = obj.GetType().GetProperties();

                int count = 0;
                foreach (PropertyInfo prop in props)
                {
                    if (count == 0) { sql += $"{prop.Name} LIKE '%{searchTxt}%' "; }
                    else { sql += $"OR {prop.Name} LIKE '%{searchTxt}%' "; }
                    count++;
                }

                return conn.Query<object>(sql).ToList();
            }
        }

        //Adds a new entry to the database
        public bool CreateObj (object obj, string tableName)
        {
            string sql1 = $"INSERT INTO {tableName} (";
            string sql2 = $"VALUES (";
            PropertyInfo[] props = obj.GetType().GetProperties();

            int count = 0;
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name != "id")
                {
                    if (count == 0) { sql1 += $"[{prop.Name}]"; sql2 += $"@{prop.Name}"; }
                    else { sql1 += $", [{prop.Name}]"; sql2 += $", @{prop.Name}"; }
                    count++;
                }
            }
            sql1 += ") ";
            sql2 += ")";

            string sql = sql1 + sql2;

            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
            {
                try
                {
                    var affectedRows = conn.Execute(sql, obj);
                    Console.WriteLine($"New entry added!");
                    Console.ReadKey();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return false;
        }

        //Updates an entry in a given table
        public bool ChangeObj (object obj, string tableName)
        {
            string sql = $"UPDATE {tableName} SET ";
            PropertyInfo[] props = obj.GetType().GetProperties();

            int count = 0;
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name != "id")
                {
                    if (count == 0) { sql += $"[{prop.Name}] = @{prop.Name}"; }
                    else { sql += $", [{prop.Name}] = @{prop.Name}"; }
                    count++;
                }
            }

            sql += $" WHERE [{props[0].Name}] = @{props[0].Name}";

            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
            {
                try
                {
                    var affectedRows = conn.Execute(sql, obj);
                    Console.WriteLine($"Entry succesfully changed!");
                    Console.ReadKey();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return false;
        }

        public bool DeleteObj (object obj, string tableName)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SydvestBo")))
            {
                PropertyInfo[] props = obj.GetType().GetProperties();
                string sql = $"DELETE FROM {tableName} WHERE [{props[0].Name}] = @{props[0].Name}";

                try
                {
                    var affectedRows = conn.Execute(sql, obj);
                    Console.WriteLine($"Entry succesfully deleted!");
                    Console.ReadKey();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
            return false;
        }
    }
}
