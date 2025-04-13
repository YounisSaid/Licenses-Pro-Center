using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsDataAccessPerson
    {
        public static string connectionString = clsConnectionString.connectionString;
        static public DataTable GetAllPepole()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Pepole_View";
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();

                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
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
        static public bool FindPersonByID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecoundName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref int Gender, ref string Address, ref string Phone,
            ref string Email, ref int NationailtyCountryId, ref string ImagePath)
        {

            bool found = false;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from People where PersonID = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", PersonID);
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                if (Reader.Read())
                {
                    PersonID = (int)Reader["PersonID"];
                    NationalNo = Reader["NationalNo"].ToString();
                    FirstName = Reader["FirstName"].ToString();
                    SecoundName = Reader["SecondName"].ToString();
                    ThirdName = Reader["ThirdName"].ToString();
                    LastName = Reader["LastName"].ToString();
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gender = Convert.ToInt32((Reader)["Gender"]);
                    Address = Reader["Address"].ToString();
                    Phone = Reader["Phone"].ToString();
                    Email = Reader["Email"].ToString();
                    NationailtyCountryId = (int)Reader["NationalityCountryID"];
                    if (Reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = Reader["ImagePath"].ToString();
                    }
                    else
                    {
                        ImagePath = "";
                    }
                    found = true;

                }
                Reader.Close();
            }
            catch
            {
                found = false;

            }
            finally
            {
                con.Close();

            }
            return found;

        }

         public static int  GetPersonId(string NationalNo)
        {
            int PersonID = -1;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select PersonID from People where NationalNo = @NationalNo";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                if (Reader.Read())
                {
                    PersonID = (int)Reader["PersonID"];
                }
                Reader.Close();
            }
            catch
            {
                PersonID = -1;
            }
            finally
            {
                con.Close();
            }
            return PersonID;
        }

        public static bool FindPersonByNationalNo(string NationalNo)
        {
            bool result = false;
            
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select H=1 from People where NationalNo = @NationalNo";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                if (Reader.HasRows )
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
        public static int AddNewPerson(string NationalNo, string FirstName, string SecoundName,
            string ThirdName, string LastName, DateTime DateOfBirth,int Gender,string Address,
            string Phone,string Email, int NationailtyCountryId, string ImagePath)
        {
            int Id = -1;
            SqlConnection con = new SqlConnection(connectionString);
            string query = @"insert into People (NationalNo,FirstName,SecondName,ThirdName,LastName,
                          DateOfBirth,Gender,Address,Phone,Email,NationalityCountryID,ImagePath)
                          values (@NationalNo,@FirstName,@SecoundName,@ThirdName,@LastName,@DateOfBirth,@Gender,@Address,@Phone,@Email,@NationailtyCountryId,@ImagePath);
                           SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecoundName", SecoundName);
            cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gender",Gender);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@NationailtyCountryId", NationailtyCountryId);
            if(ImagePath != "")
            {
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }
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
                Id = -1;
                //ex.Message.ToString();
               

            }
            finally
            {
                con.Close();
            }
            return (Id);
        }
        public static bool UpdatePerson(int Id, string NationalNo, string FirstName, string SecoundName,
            string ThirdName, string LastName, DateTime DateOfBirth,int Gender, string Address,
            string Phone, string Email, int NationailtyCountryId, string ImagePath)
        {
            bool result = false;
            SqlConnection con = new SqlConnection(connectionString);
            string query = @"update People set NationalNo = @NationalNo,FirstName = @FirstName,SecondName = @SecoundName,ThirdName = @ThirdName,LastName = @LastName
                          ,DateOfBirth = @DateOfBirth,Gender=@Gender,Address = @Address,Phone = @Phone,Email = @Email,NationalityCountryID = @NationailtyCountryId,
                           ImagePath = @ImagePath where PersonID = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", SecoundName);
            cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gender", Gender);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@NationalityCountryId", NationailtyCountryId);
            if (ImagePath != "")
            {
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }
            try
            {
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                result = (rows > 0);
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

        public static bool DeletePerson(int Id)
        {
            bool result = false;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "delete from People where PersonID = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            try
            {
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                result = (rows > 0);
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
        public static DataTable GetAllCountries()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);

            string query = "SELECT * FROM Countries order by CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch 
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }
       static public string GetCountry(int Id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select CountryName from Countries where CountryID = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            string CountryName = "";
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                if (Reader.Read())
                {
                    CountryName = Reader["CountryName"].ToString();
                }
                Reader.Close();
            }
            catch
            {
                CountryName = "";
            }
            finally
            {
                con.Close();
            }
            return CountryName;
        }
        static public DataTable FilterByPersonID(int PersonID)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Pepole_View where PersonID = @PersonID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
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

        static public DataTable FilterByNationalNo(string NationalNo)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Pepole_View where NationalNo like @NationalNo";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@NationalNo",  NationalNo );
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
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

        static public DataTable FilterByFirstName(string FirstName)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Pepole_View where FirstName like @FirstName";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FirstName",  FirstName + "%");
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
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

        static public DataTable FilterByLastName(string LastName)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Pepole_View where LastName like @LastName";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@LastName", LastName + "%");
           
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
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

        static public DataTable FilterBySecoundName(string SecoundName)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Pepole_View where SecondName like @SecoundName";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@SecoundName", SecoundName + "%");
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
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
        static public DataTable FilterByThirdName(string ThirdName)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Pepole_View where ThirdName like @ThirdName";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ThirdName", ThirdName + "%");
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
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

        static public DataTable FilterByDateOfBirth(DateTime DateOfBirth)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Pepole_View where DateOfBirth = @DateOfBirth";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth );
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
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

        static public DataTable FilterByGender(string Gender)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Pepole_View where Gender like @Gender";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Gender", Gender + "%");
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
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

        static public DataTable FilterByAddress(string Address)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Pepole_View where Address like @Address";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Address", Address + "%");
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
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

        static public DataTable FilterByPhone(string Phone)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Pepole_View where Phone like @Phone";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Phone", Phone + "%");
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
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

        static public DataTable FilterByCountry(string Country)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Pepole_View where CountryName like @Country";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Country", Country + "%");
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
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

        static public DataTable FilterByEmail(string Email)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Pepole_View where Email like @Email";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", "%" + Email + "%");
            try
            {
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
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
    }
}
