using System.Windows;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for EditEmployeeView.xaml
    /// </summary>
    public partial class EditEmployeeView : Window
    {
        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="employeeToEdit">Employee to be edited.</param>
        public EditEmployeeView(vwEmployee employeeToEdit)
        {
            InitializeComponent();
            this.DataContext = new EditEmployeeViewModel(this, employeeToEdit);
        }
    }
}
