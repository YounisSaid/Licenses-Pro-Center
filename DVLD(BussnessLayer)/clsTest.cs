using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer_;

namespace DVLD_BussnessLayer_
{
   public class clsTest
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTestAppiontment TestAppiontmentInfo;


        private enum enmode
        {
            Add,
            Update  
        }

        enmode mode; 

        public clsTest()
        {
            TestAppointmentID = -1;
            TestID = -1;
            TestResult = false;
            Notes = "";
            CreatedByUserID = -1;
            mode = enmode.Add;
            TestAppiontmentInfo = clsTestAppiontment.Find(TestAppointmentID);
            

        }

        public clsTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            TestAppiontmentInfo = clsTestAppiontment.Find(TestAppointmentID);

            mode = enmode.Update;
        }

        public static clsTest Find(int TestAppointmentID)
        {
            int TestID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;

           if(clstestData.Find(ref TestID,  TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public int Add()
        {
            return clstestData.Add(TestAppointmentID, TestResult, Notes, CreatedByUserID);
        }

        public bool Update()
        {
            return clstestData.Update(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
        }
        public bool Save()
        {
            if (mode == enmode.Add)
            {
                mode = enmode.Update;
                return (Add() !=-1);
            }
            else
            {
                return Update();
            }
        }
        public bool Delete(int TestID)
        {
            return clstestData.Delete(TestID);
        }

      static  public bool RetakeTest(int LocalDrivingLicenseApplicationID,int  TestTypeID)
        {
            return clstestData.RetakeTest( LocalDrivingLicenseApplicationID,  TestTypeID);
        }
    }
}
