using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer_
{
    public class clsDataTestTypes
    {
        public static string connectionString = clsConnectionString.connectionString;

       static public DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT * FROM TestTypes";
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.HasRows)
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

        static public bool Find(int Id, ref string TestTypeName, ref string TestTypeDescription, ref int TestTypeFees)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TestTypeID", Id);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    TestTypeName = reader["TestTypeTitle"].ToString();
                    TestTypeDescription = reader["TestTypeDescription"].ToString();
                    TestTypeFees = Convert.ToInt32(reader["TestTypeFees"]);
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

        static public bool Update(int Id, string TestTypeName, string TestTypeDescription, int TestTypeFees)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "UPDATE TestTypes SET TestTypeTitle = @TestTypeTitle, TestTypeDescription = @TestTypeDescription, TestTypeFees = @TestTypeFees WHERE TestTypeID = @TestTypeID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TestTypeID", Id);
            cmd.Parameters.AddWithValue("@TestTypeTitle", TestTypeName);
            cmd.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            cmd.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
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
