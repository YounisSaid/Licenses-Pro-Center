using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;

namespace DVLD_DataAccessLayer_
{
   public class clsDataApplicationTypes
    {
        public static string connectionString = clsConnectionString.connectionString;

        static public DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT * FROM ApplicationTypes";
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
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
                con.Close();
            }
            return dt;
        }

         static public bool UpdateApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
        {
            bool result = false;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "UPDATE ApplicationTypes SET ApplicationTypeTitle = @ApplicationTypeTitle, ApplicationFees = @ApplicationFees WHERE ApplicationTypeID = @ApplicationTypeID";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            try
            {
                con.Open();
                int rows = command.ExecuteNonQuery();
                result = (rows > 0);
              
            }
            catch
            {
                
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        static public bool FindApplicationType(int ApplicationTypeID,ref string ApplicationTypeTitle,ref decimal ApplicationFees)
        {
        
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString();
                    ApplicationFees = (decimal)reader["ApplicationFees"];
                    reader.Close();
                    return true;
                }
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
            return false;
        }
    }
}
