using System;
using System.Data.SqlClient;
using DataAccessLayer;

namespace DVLD_DataAccessLayer_
{
   public class clsLicenseData
    {
        public static string connectionString = clsConnectionString.connectionString;

        public static bool Find(int LicenseID,ref int ApplicationID,ref int DriverID,ref int LicenseClass,ref DateTime IssueDate,ref DateTime ExpirationDate,ref string Notes ,ref byte PaidFees,ref bool IsActive,ref byte IssueReason, ref int CreatedByUserID)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = (string)reader["Notes"];
                    PaidFees = Convert.ToByte(reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
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

        public static int Add(int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, byte PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int ID = -1;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "INSERT INTO Licenses (ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID) VALUES (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID);select SCOPE_IDENTITY(); ";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@Notes", Notes);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@IssueReason", IssueReason);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
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
            return (ID);
        }

       public static bool Update(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, byte PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "UPDATE Licenses SET ApplicationID = @ApplicationID, DriverID = @DriverID, LicenseClass = @LicenseClass, IssueDate = @IssueDate, ExpirationDate = @ExpirationDate, Notes = @Notes, PaidFees = @PaidFees, IsActive = @IsActive, IssueReason = @IssueReason, CreatedByUserID = @CreatedByUserID WHERE LicenseID = @LicenseID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@Notes", Notes);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@IssueReason", IssueReason);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery() > 0;
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

        public static bool Delete(int LicenseID)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "DELETE FROM Licenses WHERE LicenseID = @LicenseID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery() > 0;
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

        public static bool Lock(int LicenseID)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "UPDATE Licenses SET IsActive = 0 WHERE LicenseID = @LicenseID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery() > 0;
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

        public static bool Unlock(int LicenseID)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "UPDATE Licenses SET IsActive = 1 WHERE LicenseID = @LicenseID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery() > 0;
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
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            SqlConnection conn = new SqlConnection(connectionString);


            string query = @"SELECT        Licenses.LicenseID
                            FROM Licenses INNER JOIN
                                                     Drivers ON Licenses.DriverID = Drivers.DriverID
                            WHERE  
                             
                             Licenses.LicenseClass = @LicenseClass 
                              AND Drivers.PersonID = @PersonID
                              And IsActive=1;";

            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

            try
            {
                conn.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                conn.Close();
            }


            return LicenseID;
        }

    }
}
