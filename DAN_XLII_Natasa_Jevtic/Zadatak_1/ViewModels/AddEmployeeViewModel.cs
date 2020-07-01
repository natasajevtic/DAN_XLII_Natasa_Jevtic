using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Validations;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class AddEmployeeViewModel : BaseViewModel
    {
        AddEmployeeView addEmployeeView;
        Calculations calculator = new Calculations();
        Validation validation = new Validation();
        Employees employees = new Employees();
        Sectors sectors = new Sectors();
        Genders genders = new Genders();
        Locations locations = new Locations();

        private vwEmployee employee;

        public vwEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }
        private vwEmployee manager;

        public vwEmployee Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        private List<vwEmployee> managerList;

        public List<vwEmployee> ManagerList
        {
            get
            {
                return managerList;
            }
            set
            {
                managerList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private vwLocation location;

        public vwLocation Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }
        private List<vwLocation> locationList;

        public List<vwLocation> LocationList
        {
            get
            {
                return locationList;
            }
            set
            {
                locationList = value;
                OnPropertyChanged("LocationList");
            }
        }

        private vwGender gender;
        public vwGender Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private List<vwGender> genderList;
        public List<vwGender> GenderList
        {
            get
            {
                return genderList;
            }
            set
            {
                genderList = value;
                OnPropertyChanged("GenderList");
            }
        }

        private string sector;

        public string Sector
        {
            get
            {
                return sector;
            }
            set
            {
                sector = value;
                OnPropertyChanged("Sector");
            }
        }

        private ICommand save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private ICommand cancel;

        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        public AddEmployeeViewModel(AddEmployeeView addEmployeeView)
        {
            this.addEmployeeView = addEmployeeView;
            GenderList = genders.GetAllGender();
            LocationList = locations.GetAllLocations();
            ManagerList = employees.GetAllEmployees();
            employee = new vwEmployee();
        }
        /// <summary>
        /// This method invokes a methods for adding employee and checks if sector of employee exists. If not exist, invokes a method for adding sector.
        /// </summary>       
        public void SaveExecute()
        {
            try
            {
                if (sectors.IsSectorExists(Sector) == false)
                {
                    sectors.AddSector(sector);
                }
                //invoking method to find sector and setting that sector to employee sector
                Employee.Sector = sectors.FindSector(sector);
                Employee.LocationID = Convert.ToInt32(Location.LocationID);
                Employee.Gender = Convert.ToInt32(Gender.GenderID);
                if (Manager != null)
                {
                    Employee.Manager = Convert.ToInt32(Manager.EmployeeID);
                }
                employees.AddEmployee(employee);
                addEmployeeView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>       
        /// This method checks if user input is valid.
        /// </summary>
        /// <returns>True if data is valid, false if not.</returns>  
        public bool CanSaveExecute()
        {
            DateTime date = DateTime.Now;
            try
            {
                //checks if user input data valid
                if (!String.IsNullOrEmpty(employee.Name) && !String.IsNullOrEmpty(employee.Surname) && employee.NumberOfIdentityCard.Length == 9 && employee.NumberOfIdentityCard.All(Char.IsDigit)
                    && employee.JMBG.Length == 13 && employee.JMBG.All(Char.IsDigit) && Location != null && !String.IsNullOrEmpty(sector) && !String.IsNullOrEmpty(employee.PhoneNumber) &&
                    validation.ValidationForPhoneNumber(employee.PhoneNumber) == true && Gender != null && calculator.CalculateDateOfBirth(employee.JMBG, out date) == true
                    && validation.ValidationForUnique(employee.NumberOfIdentityCard, employee.JMBG, employee.PhoneNumber) == true)
                {
                    Employee.DateOfBirth = date;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// This method invokes method for closing window of adding employee.
        /// </summary>
        public void CancelExecute()
        {
            try
            {
                addEmployeeView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanCancelExecute()
        {
            return true;
        }

    }
}