﻿using System.Windows;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for AddEmployeeView.xaml
    /// </summary>
    public partial class AddEmployeeView : Window
    {
        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public AddEmployeeView()
        {
            InitializeComponent();
            this.DataContext = new AddEmployeeViewModel(this);
        }
    }
}
