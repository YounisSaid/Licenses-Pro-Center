using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;

namespace DVLD_DataAccessLayer_
{
    public class clsDriverData
    {
        public static string connectionString = clsConnectionString.connectionString;

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT * FROM Drivers_View;";
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public static bool Find(int DriverID,ref int PersonID, ref int CreatedByUserID,ref DateTime CreatedDate)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                    result = true;
                }
                reader.Close();
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public static int Add(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int ID = -1;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate) VALUES (@PersonID, @CreatedByUserID, @CreatedDate);select SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            try
            {
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ID = insertedID;

                }
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
            return ID;
        }

        public static bool Update(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "UPDATE Drivers SET PersonID = @PersonID, CreatedByUserID = @CreatedByUserID, CreatedDate = @CreatedDate WHERE DriverID = @DriverID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            try
            {
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static bool Delete(int DriverID)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "DELETE FROM Drivers WHERE DriverID = @DriverID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
