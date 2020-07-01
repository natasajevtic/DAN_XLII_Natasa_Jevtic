using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Zadatak_1.Models;

namespace Zadatak_1.Validations
{
    class Validation
    {
        /// <summary>
        /// This method checks if a forwarded phone number in specific format.
        /// </summary>
        /// <param name="phoneNumber">Phone number.</param>
        /// <returns>True if the phone number is in a specific format, false if not.</returns>
        public bool ValidationForPhoneNumber(string phoneNumber)
        {
            //validation for Serbian numbers
            if (Regex.Match(phoneNumber, @"^(\+3816[0-9]{7})$").Success || Regex.Match(phoneNumber, @"^(\+3816[0-9]{8})$").Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// This method checks if a forwarded number of ID card, JMBG and phone number unique in database.
        /// </summary>
        /// <param name="iDCardNumber"></param>
        /// <param name="jmbg"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public bool ValidationForUnique(string iDCardNumber, string jmbg, string phoneNumber)
        {
            Employees employees = new Employees();
            List<vwEmployee> employeeList = employees.GetAllEmployees();
            //if already exists forwarded ID card number or JMBG or phone number in database
            if (employeeList.Any(x => x.NumberOfIdentityCard == iDCardNumber) || employeeList.Any(x => x.JMBG == jmbg) || employeeList.Any(x => x.PhoneNumber == phoneNumber))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
