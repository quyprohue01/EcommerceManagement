using _19T1021203.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021203.DataLayers.SQLServer
{/// <summary>
 /// 
 /// </summary>
    public class CustomerAccountDAL : _BaseDAL, IUserAccountDAL

    {/// <summary>
     /// 
     /// </summary>
     /// <param name="connectionString"></param>
        public CustomerAccountDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserAccount Authorize(string userName, string password)
        {
            UserAccount data = null;
            using (SqlConnection connection = OpenConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @" SELECT EmployeeID , FirstName ,LastName ,Email
                                     FROM   Employees
                                     WHERE Email = @Email AND Password = @Password ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Email", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                using (var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new UserAccount()
                        {
                            UserName = Convert.ToString(dbReader["Email"]),
                            FullName = $"{dbReader["FistName"]} {dbReader["LastNmae"]}",
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = "",
                            RoleNames = "",
                        };
                    }
                    dbReader.Close();
                }
                connection.Close();
            }
            return data;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Employees
                                   SET   Password= @NewPassword
                                    WHERE Email = @Email AND Password =@OldPassword";
                cmd.Parameters.AddWithValue("@Email", userName);
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                cmd.Parameters.AddWithValue("@OldPassword", oldPassword);
                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return result;
        }
    }
}
