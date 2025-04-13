using System;
using DataAccessLayer;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_DataAccessLayer_
{
   public class clsTestAppiontementData
    {
        public static string connectionString = clsConnectionString.connectionString;

        public static DataTable GetAllTestAppiontementsforPerson(int LocalDrivingLicenseApplicationID ,int TestTypeID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM TestAppointments where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                
            }
            catch 
            {
                
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool Find(int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate,ref byte PaidFees, ref int CreatedByUserID,ref bool IsLocked)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (byte)Convert.ToSByte(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];


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

        public static int AddNewAppiontement( int TestTypeID,  int LocalDrivingLicenseApplicationID,  DateTime AppointmentDate,  byte PaidFees,  int CreatedByUserID, bool IsLocked)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO TestAppointments (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked) VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            int insertedID = -1;
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    insertedID = id;
                }
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return insertedID;
        }

        public static bool Update(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, byte PaidFees, int CreatedByUserID, bool IsLocked)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "UPDATE TestAppointments SET TestTypeID = @TestTypeID, LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID, AppointmentDate = @AppointmentDate, PaidFees = @PaidFees, CreatedByUserID = @CreatedByUserID, IsLocked = @IsLocked WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            bool isUpdated = false;
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isUpdated = true;
                }
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return isUpdated;
        }
        public static bool Delete(int TestAppointmentID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "DELETE FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            bool isDeleted = false;
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isDeleted = true;
                }
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return isDeleted;
        }

        public static bool IsLocked(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT top 1 IsLocked FROM TestAppointments where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID  order by TestAppointmentID desc";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            bool isLocked = false;
            
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                { 
                    isLocked = (bool)reader["IsLocked"];
                }

            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return isLocked;
        }

    public static bool UnLock(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"UPDATE TestAppointments
SET IsLocked = 0
WHERE TestAppointmentID = (
    SELECT TOP 1 TestAppointmentID
    FROM TestAppointments
    WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
          AND TestTypeID = @TestTypeID
    ORDER BY TestAppointmentID DESC)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            bool isUnlocked = false;
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isUnlocked = true;
                }
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return isUnlocked;
        }

    }
}
