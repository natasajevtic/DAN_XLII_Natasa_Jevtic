using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class Genders
    {
        /// <summary>
        /// This method creates a list of data from view of genders.
        /// </summary>
        /// <returns></returns>
        public List<vwGender> GetAllGender()
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    List<vwGender> genders = new List<vwGender>();
                    genders = (from x in context.vwGenders select x).ToList();
                    return genders;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
