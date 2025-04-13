using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer_
{
    public class clsDataAccessUser
    {
        public static string connectionString = clsConnectionString.connectionString;

        static public DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Users_View";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
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

        static public bool AddUser(int PersonID, string UserName, string Password, int IsActive)
        {
            int Id = -1;
            SqlConnection con = new SqlConnection(connectionString);
            string query = @"insert into Users (PersonID,UserName,Password,IsActive) values (@PersonID,@UserName,@Password,@isActive);
                           Select SCOPE_IDENTITY(); ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@isActive", IsActive);
            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();


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
                con.Close();
            }
            return (Id != -1);
        }




        static public bool UpdateUser(int Id, int PersonID, string UserName, string Password, int IsActive)
        {
            bool Updated = false;
            SqlConnection con = new SqlConnection(connectionString);
            string query = @"update Users set PersonID=@PersonID,UserName=@UserName,Password=@Password,IsActive=@isActive where UserID=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@isActive", IsActive);
            try
            {
                
                    con.Open();
                    
                
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Updated = true;
                }

            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
            return Updated;
        }

        public static bool DeleteUser(int Id)
        {
            bool Deleted = false;
            SqlConnection con = new SqlConnection(connectionString);
            string query = @"delete from Users where UserID=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            try
            {
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Deleted = true;
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
            return Deleted;
        }

        public static bool GetUserById(int Id, ref int PersonId, ref string UserName, ref string Password, ref int IsActive)
        {
            bool found = false;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Users where UserID=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    PersonId = (int)reader["PersonID"];
                    UserName = reader["UserName"].ToString();
                    Password = reader["Password"].ToString();
                    IsActive = Convert.ToInt32(reader["IsActive"]);
                    found = true;
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
            return found;
        }


        public static bool FindUserByPersonID(int PersonID)
        {
            int Id = -1;

            SqlConnection con = new SqlConnection(connectionString);
            string query = "select h=1 from Users where PersonID=@PersonID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    Id = id;
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
            return (Id != -1);
        }


        static public bool IsUserExistsl(string userName)
        {
            bool found = false;
            string query = "SELECT 1 FROM Users WHERE UserName = @UserName";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = userName; // Avoid AddWithValue

                try
                {
                    con.Open();
                    using (SqlDataReader result = cmd.ExecuteReader())
                    {
                        found = result.HasRows; // More efficient than Read()
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database Error: " + ex.Message); // Log the error
                }
            }

            return found;
        }
        public static bool IsUserExistss(string userName)
        {
            bool result = false;

            SqlConnection con = new SqlConnection(connectionString);
            string query = "select H=1 from  users where UserName = @userName";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userName", userName);
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                if (Reader.HasRows)
                {
                    result = true;
                }
                Reader.Close();

            }
            catch
            {
                result = false;
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public static bool IsUserExists(string userName)
        {
            string query = "SELECT COUNT(1) FROM Users WHERE UserName = @userName";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add("@userName", SqlDbType.NVarChar, 50).Value = userName;

                try
                {
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database Error: " + ex.Message);
                    return false;
                }
            }
        }

        public static int GetUserByUserName(string userName)
        {
            int result = -1;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select UserID from Users where UserName = @userName";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userName", userName);
            try
            {
                con.Open();
                object obj = cmd.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int id))
                {
                    result = id;
                }
            }
            catch
            {
                result = -1;
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public static bool PasswordMatches(int userId, string password)
        {
            bool result = false;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select 1 from Users where UserID = @userId and Password = @password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@password", password);
            try
            {
                con.Open();
                object obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public static DataTable FilterByUserId(int UserId)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Users_View where UserID like UserId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", UserId );
            try
            {
                con.Open();
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
                con.Close();
            }
            return dt;
        }

        public static DataTable FilterByPersonID(int PersonID)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Users_View where PersonID like @PersonID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@PersonID", PersonID );
            try
            {
                con.Open();
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
                con.Close();
            }
            return dt;
        }

        static public DataTable FilterByUserName(string UserName)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Users_View where UserName like @UserName";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserName", UserName + "%");
            try
            {
                con.Open();
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
                con.Close();
            }
            return dt;
        }

        static public DataTable FilterByFullName(string FullName)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Users_View where FullName like @FullName";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FullName", FullName + "%");
            try
            {
                con.Open();
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
                con.Close();
            }
            return dt;
        }

        static public DataTable FilterByIsActive(int  IsActive)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Users_View where IsActive=@IsActive";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
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

        static public int Login(string userName, string password)
        {
            int UserId = -1;    
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select UserID from Users where UserName = @userName and Password = @password and IsActive = 1;";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@password", password);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() )
                {
                    UserId = reader.GetInt32(0);
                }
            }
            catch
            {
                
            }
            finally
            {
                con.Close();
            }
            return UserId;
        }


    }
}
