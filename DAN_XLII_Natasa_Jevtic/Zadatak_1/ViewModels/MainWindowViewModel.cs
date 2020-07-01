using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;

namespace Zadatak_1.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        MainWindow main;
        Employees employees = new Employees();
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


        private List<vwEmployee> employeeList;
        public List<vwEmployee> EmployeeList
        {
            get
            {
                return employeeList;
            }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private ICommand deleteEmployee;

        public ICommand DeleteEmployee
        {
            get
            {
                if (deleteEmployee == null)
                {
                    deleteEmployee = new RelayCommand(param => DeleteEmployeeExecute(), param => CanDeleteEmployeeExecute());
                }
                return deleteEmployee;
            }

        }

        private ICommand editEmployee;

        public ICommand EditEmployee
        {
            get
            {
                if (editEmployee == null)
                {
                    editEmployee = new RelayCommand(param => EditEmployeeExecute(), param => CanEditEmployeeExecute());
                }
                return editEmployee;
            }
        }

        private ICommand addEmployee;

        public ICommand AddEmployee
        {
            get
            {
                if (addEmployee == null)
                {
                    addEmployee = new RelayCommand(param => AddEmployeeExecute(), param => CanAddEmployeeExecute());
                }
                return addEmployee;
            }
        }

        public MainWindowViewModel(MainWindow main)
        {
            this.main = main;
            //invoking method to fill a list of employees
            EmployeeList = employees.GetAllEmployees();
            //invoking method for reading locations from file
            locations.AddLocations();
        }
        /// <summary>
        /// This method asks for confirmation to delete. If confirmed, methods for deleting employees is invoked.
        /// </summary>
        public void DeleteEmployeeExecute()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure that you want to delete the employee?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    if (Employee != null)
                    {
                        //invokeing method for deleting employees
                        employees.DeleteEmployee(Employee.EmployeeID);                       
                        //invoking method to update list of employees
                        EmployeeList = employees.GetAllEmployees();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public bool CanDeleteEmployeeExecute()
        {
            return true;
        }

        public void EditEmployeeExecute()
        {
            
        }

        public bool CanEditEmployeeExecute()
        {
            return true;
        }

        public void AddEmployeeExecute()
        {
            
        }

        public bool CanAddEmployeeExecute()
        {
            return true;
        }
    }
}
