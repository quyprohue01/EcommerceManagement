using _19T1021203.DataLayers.SQLServer;
using _19T1021203.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace _19T1021203.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt xử lý dữ liệu liên quan đến quốc gia
    /// </summary>
    public class CountryDAL : _BaseDAL, ICountryDAL
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public CountryDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Country> list()
        {
            List<Country> data = new List<Country>();
            using (var connection = OpenConnection())
            {

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT CountryName FROM Countries";
                cmd.CommandType = CommandType.Text;

                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dbReader.Read())
                {
                    data.Add(new Country()
                    {
                        CountryName = Convert.ToString(dbReader["CountryName"])
                    }); ;
                }
                dbReader.Close();
                connection.Close();

            }
            return data;
        }
    }
}
