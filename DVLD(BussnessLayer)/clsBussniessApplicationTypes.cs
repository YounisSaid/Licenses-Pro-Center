using System;
using System.Data;
using System.Runtime.CompilerServices;
using DVLD_DataAccessLayer_;
namespace DVLD_BussnessLayer_
{
    public class clsBussniessApplicationTypes
    {
        public int ApplicationTypeID { get; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }
        public clsBussniessApplicationTypes(int ID, string ApplicationTypeTitle, decimal ApplicationFees)
        {
           

            this.ApplicationTypeID = ID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
        }

        public static DataTable GetAllApplicationTypes()
        {     
            return clsDataApplicationTypes.GetAllApplicationTypes();
        }

        public  bool UpdateApplicationType()
        {
            return clsDataApplicationTypes.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle,this.ApplicationFees);
        }

        public static clsBussniessApplicationTypes FindApplicationType(int ApplicationTypeID)
        {
           string ApplicationTypeTitle = "";
            decimal ApplicationFees = 0;
            if( clsDataApplicationTypes.FindApplicationType(ApplicationTypeID,ref ApplicationTypeTitle,ref ApplicationFees))
            {
                return new clsBussniessApplicationTypes(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            else
            {
                return null;
            }
        }
    }
}
