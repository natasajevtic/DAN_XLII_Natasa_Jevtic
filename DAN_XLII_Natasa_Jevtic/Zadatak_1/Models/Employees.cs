﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class Employees : Logger
    {
        /// <summary>
        /// This method adds employee to DbSet and then save changes to database.
        /// </summary>
        /// <param name="employeeToAdd">Employee.</param>
        public void AddEmployee(vwEmployee employeeToAdd)
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    tblEmployee employee = new tblEmployee
                    {
                        Name = employeeToAdd.Name,
                        Surname = employeeToAdd.Surname,
                        DateOfBirth = employeeToAdd.DateOfBirth,
                        NumberOfIdentityCard = employeeToAdd.NumberOfIdentityCard,
                        JMBG = employeeToAdd.JMBG,
                        Gender = employeeToAdd.Gender,
                        PhoneNumber = employeeToAdd.PhoneNumber,
                        Sector = employeeToAdd.Sector,
                        LocationID = employeeToAdd.LocationID,
                        Manager = employeeToAdd.Manager
                    };
                    context.tblEmployees.Add(employee);
                    context.SaveChanges();
                    employeeToAdd.EmployeeID = employee.EmployeeID;
                    LogAction("Employee " + employeeToAdd.Employee + " created. ID: " + employeeToAdd.EmployeeID + " JMBG: " + employeeToAdd.JMBG);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// This method creates a list of data from view of employees.
        /// </summary>
        /// <returns>List of employees.</returns>
        public List<vwEmployee> GetAllEmployees()
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    List<vwEmployee> employees = new List<vwEmployee>();
                    employees = (from x in context.vwEmployees select x).ToList();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method edits employee in DbSet and then saves changes to database.
        /// </summary>
        /// <param name="employee">Employee to edit.</param>
        /// <returns>Edited employee.</returns>
        public vwEmployee EditEmployee(vwEmployee employee)
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    tblEmployee employeeToEdit = context.tblEmployees.Where(x => x.EmployeeID == employee.EmployeeID).FirstOrDefault();
                    employeeToEdit.Name = employee.Name;
                    employeeToEdit.Surname = employee.Surname;
                    employeeToEdit.DateOfBirth = employee.DateOfBirth;
                    employeeToEdit.NumberOfIdentityCard = employee.NumberOfIdentityCard;
                    employeeToEdit.Gender = employee.Gender;
                    employeeToEdit.PhoneNumber = employee.PhoneNumber;
                    employeeToEdit.Sector = employee.Sector;
                    employeeToEdit.LocationID = employee.LocationID;
                    employeeToEdit.Manager = employee.Manager;
                    context.SaveChanges();
                    LogAction("Employee with ID " + employeeToEdit.EmployeeID + " updated.");
                    return employee;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method deletes employee from DbSet and then saves changes to database.
        /// </summary>
        /// <param name="employeeID">ID of employee.</param>
        public void DeleteEmployee(int employeeID)
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    //creating a list of employees for which employee with forwarded id is the manager
                    var employeeOfThisManager = context.tblEmployees.Where(x => x.Manager == employeeID).ToList();
                    //if the list is not empty, setting manager id to null for every employee in that list
                    if (employeeOfThisManager.Count() > 0)
                    {
                        foreach (var employee in employeeOfThisManager)
                        {
                            employee.Manager = null;
                            LogAction("Employee with ID " + employee.EmployeeID + " updated so he has no manager.");
                        }
                    }
                    //finding employee with forwarded id
                    tblEmployee employeeToDelete = context.tblEmployees.Where(x => x.EmployeeID == employeeID).FirstOrDefault();
                    //if that employee was the only in the sector, deleting sector
                    var peopleInSector = context.tblEmployees.Where(x => x.Sector == employeeToDelete.Sector).ToList();
                    if (peopleInSector.Count()==1)
                    {
                        var sector = context.tblSectors.Where(x => x.SectorID == employeeToDelete.Sector).FirstOrDefault();
                        context.tblSectors.Remove(sector);
                        context.SaveChanges();
                        LogAction("Sector " + sector.SectorName + " with ID: " + sector.SectorID + " deleted.");
                    }
                    //removing employee from DbSet and saving changes to database
                    context.tblEmployees.Remove(employeeToDelete);                    
                    context.SaveChanges();
                    LogAction("Employee with ID " + employeeToDelete.EmployeeID + " deleted.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// This method creates a list of possible managers of employee.
        /// </summary>
        /// <param name="employee">Employee.</param>
        /// <returns>List of possible managers.</returns>
        public List<vwEmployee> GetAllManagers(vwEmployee employee)
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    List<vwEmployee> employees = new List<vwEmployee>();
                    //inserting into list all employees except forwarded employee (finding him based on jmbg)
                    employees = context.vwEmployees.Where(x => x.JMBG != employee.JMBG).ToList();
                    return employees;
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
