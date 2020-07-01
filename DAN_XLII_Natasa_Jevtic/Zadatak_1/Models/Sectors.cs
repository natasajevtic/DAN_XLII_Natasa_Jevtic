using System;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class Sectors
    {
        /// <summary>
        /// This method adds sectors to DbSet and then save changes to database.
        /// </summary>
        /// <param name="sectorToAdd">Sector name.</param>
        public void AddSector(string sectorToAdd)
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    tblSector sector = new tblSector
                    {
                        SectorName = sectorToAdd
                    };
                    context.tblSectors.Add(sector);
                    context.SaveChanges();                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// This method checks if sector already exists in database.
        /// </summary>
        /// <param name="sectorName">Sector name.</param>
        /// <returns>True if sector exists, false if not.</returns>
        public bool IsSectorExists(string sectorName)
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    tblSector sector = context.tblSectors.Where(x => x.SectorName == sectorName).FirstOrDefault();
                    if (sector != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method finds sector in DbSet based on a forwared name.
        /// </summary>
        /// <param name="sectorName"></param>
        /// <returns>Id of sector.</returns>
        public int FindSector(string sectorName)
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    return context.vwSectors.Where(x => x.SectorName == sectorName).Select(x => x.SectorID).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return 0;
            }
        }
    }
}
