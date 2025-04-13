using System;

using System.Data;
using DataAccessLayer;
using DVLD_DataAccessLayer_;


namespace DVLD_BussnessLayer_
{
    public class clsBussniessUser
    {
        public int UserId { get;  }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }
        enum enMode { AddNew, Update }
        enMode mode;
        static public DataTable GetAllUsers()
        {
            return clsDataAccessUser.GetAllUsers();
        }

       public clsBussniessUser(int UserId, int PersonID, string UserName, string Password, int IsActive)
        {
            mode = enMode.Update;
            this.UserId = UserId;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
        }
        public clsBussniessUser()
        {
            mode = enMode.AddNew;
            this.UserId = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = -1;
        }
        private bool _AddUser()
        {
            return clsDataAccessUser.AddUser(this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        private bool _UpdateUser()
        {
            return clsDataAccessUser.UpdateUser(this.UserId, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        public bool Save()
        {
            if (mode == enMode.AddNew)
            {
                if (_AddUser())
                {
                    mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return _UpdateUser();
            }
        }

       static public clsBussniessUser GetUserById(int Id)
        {
            int PersonId = -1;
            string UserName = "";
            string Password = "";
            int IsActive = -1;
            if (clsDataAccessUser.GetUserById(Id, ref PersonId, ref UserName, ref Password, ref IsActive))
            {
                return new clsBussniessUser(Id,  PersonId,  UserName,  Password, IsActive);
            }
            return null;
        }

        static public bool DeleteUser(int id)
        {
            return clsDataAccessUser.DeleteUser(id);
        }

        public static bool FindUserByPersonID(int PersonID)
        {
            return (clsDataAccessUser.FindUserByPersonID(PersonID));
        }

        public static bool isUserExists(string UserName)
        {
            return clsDataAccessUser.IsUserExists(UserName);
        }

        public static int GetUserByUserName(string UserName)
        {
            return clsDataAccessUser.GetUserByUserName(UserName);
        }

        public static bool PasswordMatches(int UserId,string Password)
        {
            return clsDataAccessUser.PasswordMatches(UserId, Password);
        }

        public static DataTable FilterByUserId(int UserId)
        {
            return clsDataAccessUser.FilterByUserId(UserId);
        }

        public static DataTable FilterByPersonId(int PersonId)
        {
            return clsDataAccessUser.FilterByPersonID(PersonId);
        }
        public static DataTable FilterByUserName(string UserName)
        {
            return clsDataAccessUser.FilterByUserName(UserName);
        }

        public static DataTable FilterByFullName(string FullName)
        {
            return clsDataAccessUser.FilterByFullName(FullName);
        }

        public static DataTable FilterByIsActive(int IsActive)
        {
            return clsDataAccessUser.FilterByIsActive(IsActive);
        }

        public static int Login(string UserName, string Password)
        {
            return clsDataAccessUser.Login(UserName, Password);
        }

    }
}
