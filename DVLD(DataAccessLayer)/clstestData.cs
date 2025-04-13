using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataAccessLayer;


namespace DVLD_DataAccessLayer_
{
    public class clstestData
    {
        public static string connectionString = clsConnectionString.connectionString;

        public static bool Find(ref int TestID,  int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Tests WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestID = (int)reader["TestID"];
                    TestResult = (bool)reader["TestResult"];
                    Notes = (string)reader["Notes"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static int Add(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int Id = -1;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID) VALUES (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    Id = insertedID;

                }
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return Id;
        }
        public static bool Update(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "UPDATE Tests SET TestAppointmentID = @TestAppointmentID, TestResult = @TestResult, Notes = @Notes, CreatedByUserID = @CreatedByUserID WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    isSuccess = true;
                }
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
        public static bool Delete(int TestID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "DELETE FROM Tests WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    isSuccess = true;
                }
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
       public static bool RetakeTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT top 1 h=1 
FROM TestAppointments 
JOIN Tests ON TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
          AND TestAppointments.TestTypeID = @TestTypeID 
          AND Tests.TestResult = 0 
ORDER BY TestAppointments.TestAppointmentID DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isSuccess = true;
                }


            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }

    }
}
