using System;
using System.Data;
using DVLD_DataAccessLayer_;
namespace DVLD_BussnessLayer_
{
    public class clsTestAppiontment
    {
        public int TestAppointmentID { get; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public byte PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public  clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication ;
        enum enMode { AddNew = 0, Update = 1 };
        enMode Mode;
        enum enTestType { Vision =1 ,Written = 2, Driving = 3 };
        enTestType TestType;

        public clsTestAppiontment()
        {
            TestAppointmentID = -1;
            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            IsLocked = false;
            Mode = enMode.AddNew;
        }
        public clsTestAppiontment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, byte PaidFees, int CreatedByUserID, bool IsLocked)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            Mode = enMode.Update;
        }
        public static DataTable GetAllTestAppiontementsforPerson(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppiontementData.GetAllTestAppiontementsforPerson(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        private bool AddNewAppiontement()
        {
            return (clsTestAppiontementData.AddNewAppiontement(TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked) != -1);
        }

        private bool Update()
        {
            return clsTestAppiontementData.Update(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
        }

        public static clsTestAppiontment Find(int TestAppointmentID)
        {
            
            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            byte PaidFees = 0;
            int CreatedByUserID = -1;
            bool IsLocked = false;

            if (clsTestAppiontementData.Find(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked))
            {
                return new clsTestAppiontment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            }
            return null;
        }

        public static bool Delete(int TestAppointmentID)
        {
            return clsTestAppiontementData.Delete(TestAppointmentID);
        }

        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                Mode = enMode.Update;
               return AddNewAppiontement();
            }
            else
            {
               return Update();
            }
        }


        public static bool IsLastappLocked(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppiontementData.IsLocked(LocalDrivingLicenseApplicationID, TestTypeID);
        }
        public static bool UnLock(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppiontementData.UnLock(LocalDrivingLicenseApplicationID, TestTypeID);
        }

    }
}
