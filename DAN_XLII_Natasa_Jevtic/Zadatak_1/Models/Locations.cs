using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Zadatak_1.Models
{
    class Locations
    {
        /// <summary>
        /// This method reads locations from file, adds them to DbSet and then save changes to database.
        /// </summary>
        public void AddLocations()
        {
            try
            {
                string[] lines = File.ReadAllLines(@"../../Lokacije.txt");
                List<string> list = new List<string>();
                for (int i = 0; i < lines.Length; i++)
                {
                    tblLocation location = new tblLocation();
                    list = lines[i].Split(',').ToList();
                    location.Address = list[0];
                    location.City = list[1];
                    location.State = list[2];
                    using (Employee_DataEntities context = new Employee_DataEntities())
                    {
                        //checking if location already exists
                        if (context.tblLocations.Where(x => x.Address == location.Address && x.City == location.City && x.State == location.State).ToList().Count == 0)
                        {
                            //if not exists, adding location to DbSet and saving changes to database
                            context.tblLocations.Add(location);
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// This method creates a list of data from view of locations.
        /// </summary>
        /// <returns>List of locations.</returns>
        public List<vwLocation> GetAllLocations()
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    List<vwLocation> locations = new List<vwLocation>();
                    locations = (from x in context.vwLocations select x).OrderBy(x => x.Address).ToList();
                    return locations;
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
