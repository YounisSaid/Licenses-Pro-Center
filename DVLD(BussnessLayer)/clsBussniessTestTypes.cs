using System;
using System.Collections.Generic;
using System.Data;
using DVLD_DataAccessLayer_;

namespace DVLD_BussnessLayer_
{
    public class clsBussniessTestTypes
    {
        public int ID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public int TestTypeFees { get; set; }
        public clsBussniessTestTypes(int iD, string testTypeTitle, string testTypeDescription, int testTypeFees)
        {
            ID = iD;
            TestTypeTitle = testTypeTitle;
            TestTypeDescription = testTypeDescription;
            TestTypeFees = testTypeFees;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsDataTestTypes.GetAllTestTypes();
        }

        public static clsBussniessTestTypes Find(int Id)
        {
           
            string TestTypeName = "";
            string TestTypeDescription = "";
            int TestTypeFees = -1;
            if (clsDataTestTypes.Find(Id, ref TestTypeName, ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsBussniessTestTypes(Id, TestTypeName, TestTypeDescription, TestTypeFees);
            }
            return null;
        }

        public bool Update()
        {
            return clsDataTestTypes.Update(this.ID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }
    }
}
