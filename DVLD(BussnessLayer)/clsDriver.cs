using System;
using System.Data;
using DVLD_DataAccessLayer_;

namespace DVLD_BussnessLayer_
{
   public class clsDriver
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public enum enMode
        {
            Add,
            Update
           
        }
        enMode _Mode;
        public clsDriver()
        {
           this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            _Mode = enMode.Add;

        }
        public clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            DriverID = driverID;
            PersonID = personID;
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;
            _Mode = enMode.Update;
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }
        public static clsDriver Find(int DriverID)
        {
            bool result = false;
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.Find(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            return null;
        }
        public int Add()
        {
            return clsDriverData.Add(PersonID, CreatedByUserID, CreatedDate) ;
        }
        public bool Update()
        {
            return clsDriverData.Update(DriverID, PersonID, CreatedByUserID, CreatedDate);
        }
        
        public bool Delete(int DriverID)
        {
            return clsDriverData.Delete(DriverID);
        }
    }
}
