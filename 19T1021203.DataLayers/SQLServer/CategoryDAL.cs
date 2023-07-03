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
    public class CategoryDAL : _BaseDAL, ICommonDAL<Category>
    {
        public CategoryDAL(string connectionString) : base(connectionString)
        {

        }

        public int Add(Category data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Categories(CategoryName, Description,ParentCategoryId)
                                    VALUES(@CategoryName, @Description,@ParentCategoryId);
                                     SELECT SCOPE_IDENTITY()";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@ParentCategoryId", data.ParentCategoryId);
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
                                    FROM	Categories 
                                    WHERE	(@SearchValue = N'')
	                                    OR	(
			                                    (CategoryName LIKE @SearchValue) 
                                             OR (Description LIKE @SearchValue)
                                             OR (ParentCategoryId LIKE @SearchValue)    
			                                
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
                cmd.CommandText = @"DELETE FROM Categories
                                    WHERE CategoryID = @CategoryID AND NOT EXISTS(SELECT * FROM Products WHERE CategoryID = @CategoryID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@CategoryID", id);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        public Category Get(int id)
        {
            Category data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Categories WHERE CategoryID = @CategoryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@CategoryID", id);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    if (dbReader["ParentCategoryId"].ToString().Equals(""))
                    {
                        data = new Category()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            ParentCategoryId = 0,

                        };
                    }
                    else
                    {
                        data = new Category()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            ParentCategoryId = Convert.ToInt32(dbReader["ParentCategoryId"]),

                        };
                    }

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
                                                WHEN EXISTS(SELECT * FROM Products WHERE CategoryID = @CategoryID) THEN 1 
                                                ELSE 0 
                                            END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@CategoryID", id);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        public IList<Category> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Category> data = new List<Category>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (var cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM 
                                    (
	                                    SELECT	*, ROW_NUMBER() OVER (ORDER BY CategoryName) AS RowNumber
	                                    FROM	Categories 
	                                    WHERE	(@SearchValue = N'')
		                                    OR	(
				                                    (CategoryName LIKE @SearchValue)
			                                     OR (Description LIKE @SearchValue) 
                                                 OR (ParentCategoryId LIKE @SearchValue)
                                                 
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

                    data.Add(new Category()
                    {
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        CategoryName = Convert.ToString(dbReader["CategoryName"]),
                        Description = Convert.ToString(dbReader["Description"]),
                        ParentCategoryId = 0


                    });
                }

                dbReader.Close();
                cn.Close();
            }
            return data;
        }

        public bool Update(Category data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                if (data.ParentCategoryId != 0)
                {
                    cmd.CommandText = @"UPDATE Categories
                                    SET CategoryName = @CategoryName, Description = @Description ,ParentCategoryId = @ParentCategoryId
                                    WHERE CategoryID = @CategoryID";

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;

                    cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                    cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                    cmd.Parameters.AddWithValue("@Description", data.Description);

                    cmd.Parameters.AddWithValue("@ParentCategoryId", data.ParentCategoryId);
                }
                else
                {
                    cmd.CommandText = @"UPDATE Categories
                                    SET CategoryName = @CategoryName, Description = @Description 
                                    WHERE CategoryID = @CategoryID";

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;

                    cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                    cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                    cmd.Parameters.AddWithValue("@Description", data.Description);


                }

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
