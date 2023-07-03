using _19T1021203.DataLayers.SQLServer;
using _19T1021203.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021203.DataLayers.SQLServer
{
    public class EmployeeDAL : _BaseDAL, ICommonDAL<Employee>
    {
        public EmployeeDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Employee data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Employees(LastName, FirstName, BirthDate, Photo, Notes, Email,Password)
                                    VALUES(@LastName, @FirstName, @BirthDate, @Photo, @Notes, @Email,@Password);
                                     SELECT SCOPE_IDENTITY()";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Note);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", "");
                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        public int Count(string searchValue = "")
        {
            int count = 0;

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	COUNT(*)
                                    FROM	Employees 
                                    WHERE	(@SearchValue = N'')
	                                    OR	(
			                                    (LastName LIKE @SearchValue)
			                                    OR (FirstName LIKE @SearchValue)		                                   
                                                OR (Email LIKE @SearchValue)
		                                    )";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return count;
        }

        public bool Delete(int id)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Employees
                                    WHERE EmployeeID = @EmployeeID AND NOT EXISTS(SELECT * FROM Orders WHERE EmployeeID = @EmployeeID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@EmployeeID", id);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        public Employee Get(int id)
        {
            Employee data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Employee()
                    {
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        LastName = Convert.ToString(dbReader["LastName"]),
                        FirstName = Convert.ToString(dbReader["FirstName"]),
                        BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        Note = Convert.ToString(dbReader["Notes"]),
                        Email = Convert.ToString(dbReader["Email"]),
                        Password = Convert.ToString(dbReader["Password"])
                    };
                }
                cn.Close();
            }
            return data;
        }

        public bool InUsed(int id)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE 
                                                WHEN EXISTS(SELECT * FROM Orders WHERE EmployeeID = @EmployeeID) THEN 1 
                                                ELSE 0 
                                            END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@EmployeeID", id);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        public IList<Employee> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Employee> data = new List<Employee>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (var cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM 
                                    (
	                                    SELECT	*, ROW_NUMBER() OVER (ORDER BY LastName) AS RowNumber
	                                    FROM	Employees 
	                                    WHERE	(@SearchValue = N'')
		                                    OR	(
				                                    (LastName LIKE @SearchValue)
			                                     OR (FirstName LIKE @SearchValue)                                 
                                                 OR (Email LIKE @SearchValue)
			                                    )
                                    ) AS t
                                    WHERE (@PageSize = 0) OR (t.RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize);";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@Page", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Employee()
                    {
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        LastName = Convert.ToString(dbReader["LastName"]),
                        FirstName = Convert.ToString(dbReader["FirstName"]),
                        BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        Note = Convert.ToString(dbReader["Notes"]),
                        Email = Convert.ToString(dbReader["Email"]),

                    });
                }

                dbReader.Close();
                cn.Close();
            }
            return data;
        }

        public bool Update(Employee data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                if (data.Photo != null)
                {
                    cmd.CommandText = @"UPDATE Employees
                                    SET LastName = @LastName, FirstName = @FirstName, BirthDate = @BirthDate, Photo = @Photo, Notes = @Notes, Email = @Email
                                    WHERE EmployeeID = @EmployeeID";

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                    cmd.Parameters.AddWithValue("@LastName", data.LastName);
                    cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                    cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                    cmd.Parameters.AddWithValue("@Photo", data.Photo);
                    cmd.Parameters.AddWithValue("@Notes", data.Note);
                    cmd.Parameters.AddWithValue("@Email", data.Email);
                }
                else
                {
                    cmd.CommandText = @"UPDATE Employees
                                    SET LastName = @LastName, FirstName = @FirstName, BirthDate = @BirthDate,  Notes = @Notes, Email = @Email
                                    WHERE EmployeeID = @EmployeeID";

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                    cmd.Parameters.AddWithValue("@LastName", data.LastName);
                    cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                    cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);

                    cmd.Parameters.AddWithValue("@Notes", data.Note);
                    cmd.Parameters.AddWithValue("@Email", data.Email);
                }


                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }



}
