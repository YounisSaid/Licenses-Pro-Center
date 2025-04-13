using System;
using System.Data;
using System.Net.Http;
using System.Threading;
using DataAccessLayer;

namespace BussnessLayer
{
    public class clsBussinessPerson
    {
        public int ID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecoundName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + SecoundName + " " + ThirdName + " " + LastName;
            }
        }
        public DateTime DateOfBirth { get; set; }

        public int Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationailtyCountryId { get; set; }
        public string ImagePath { get; set; }

        enum enMode { AddNew, Update }
        enMode Mode;

        public clsBussinessPerson()
        {
            Mode = enMode.AddNew;
            this.ID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecoundName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationailtyCountryId = -1;
            this.ImagePath = "";
            this.Gender = -1;


        }
        public clsBussinessPerson(int Id, string NationalNo, string FirstName, string SecoundName,
            string ThirdName, string LastName, DateTime DateOfBirth, int Gender, string Address, string Phone,
            string Email, int NationailtyCountryId, string ImagePath)
        {
            Mode = enMode.Update;
            this.ID = Id;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecoundName = SecoundName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationailtyCountryId = NationailtyCountryId;
            this.ImagePath = ImagePath;
            this.Gender = Gender;
        }
        static public DataTable GetAllPepole()
        {

            return clsDataAccessPerson.GetAllPepole();
        }

        static public clsBussinessPerson FindPersonByID(int Id)
        {

            int ID = Id;
            string NationalNo = "";
            string FirstName = "";
            string SecoundName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationailtyCountryId = -1;
            string ImagePath = "";
            int Gender = -1;

            if (clsDataAccessPerson.FindPersonByID(Id, ref NationalNo, ref FirstName, ref SecoundName,
                ref ThirdName, ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref NationailtyCountryId, ref ImagePath))
            {
                return new clsBussinessPerson(Id, NationalNo, FirstName, SecoundName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationailtyCountryId, ImagePath);
            }
            return null;
        }
        

        public bool _AddPerson()
        {

            return (clsDataAccessPerson.AddNewPerson(this.NationalNo, this.FirstName, this.SecoundName, this.ThirdName, this.LastName, this.DateOfBirth,
                this.Gender, this.Address, this.Phone, this.Email, this.NationailtyCountryId, this.ImagePath) != -1);

        }

        public bool _UpdatePerson()
        {

            return clsDataAccessPerson.UpdatePerson(this.ID, this.NationalNo, this.FirstName, this.SecoundName, this.ThirdName, this.LastName, this.DateOfBirth,
                this.Gender, this.Address, this.Phone, this.Email, this.NationailtyCountryId, this.ImagePath);
        }

        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                if(_AddPerson())
                {
                    
                    Mode = enMode.Update;
                    return true;
                }
                return false;
            }
            else
            {
                return _UpdatePerson();
            }
        }

        static public bool DeletePerson(int Id)
        {
            return clsDataAccessPerson.DeletePerson(Id);
        }

        public static DataTable GetAllCountries()
        {
            return clsDataAccessPerson.GetAllCountries();
        }
        public static string GetCountryName(int CountryId)
        {
            return clsDataAccessPerson.GetCountry(CountryId);
        }

        public static bool CheckNationalNo(string NationalNo)
        {
            return clsDataAccessPerson.FindPersonByNationalNo(NationalNo);
        }

        public static int   GetPersonId(string NationalNo)
        {
            return clsDataAccessPerson.GetPersonId(NationalNo);
        }
        public static DataTable FilterByPersonID(int PersonId)
        {
            return clsDataAccessPerson.FilterByPersonID(PersonId);
        }
        public static DataTable FilterByNationalNo(string NationalNo)
        {
            return clsDataAccessPerson.FilterByNationalNo(NationalNo);
        }

        public static DataTable FilterByLastName(string LastName)
        {
            return clsDataAccessPerson.FilterByLastName(LastName);
        }

        public static DataTable FilterByCountry(string Country)
        {
            return clsDataAccessPerson.FilterByCountry(Country);
        }

        public static DataTable FilterBySecoundName(string SecoundName)
        {
            return clsDataAccessPerson.FilterBySecoundName(SecoundName);
        }
        public static DataTable FilterByThirdName(string ThirdName)
        {
            return clsDataAccessPerson.FilterByThirdName(ThirdName);
        }
        public static DataTable FilterByFirstName(string FirstName)
        {
            return clsDataAccessPerson.FilterByFirstName(FirstName);
        }
        public static DataTable FilterByDateOfBirth(DateTime DateOfBirth)
        {
            return clsDataAccessPerson.FilterByDateOfBirth(DateOfBirth);
        }
        public static DataTable FilterByGender(string Gender)
        {
            return clsDataAccessPerson.FilterByGender(Gender);
        }
        public static DataTable FilterByAddress(string Address)
        {
            return clsDataAccessPerson.FilterByAddress(Address);
        }
        public static DataTable FilterByPhone(string Phone)
        {
            return clsDataAccessPerson.FilterByPhone(Phone);
        }
        public static DataTable FilterByEmail(string Email)
        {
            return clsDataAccessPerson.FilterByEmail(Email);
        }

    }
}
