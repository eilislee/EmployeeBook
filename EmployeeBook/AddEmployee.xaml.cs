using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using EmployeeBook;


namespace EmployeeBook
{
    public sealed partial class AddEmployee : Page
    {
        public AddEmployee()
        {
            this.InitializeComponent();
            
        }
        private void Add_Click(object sender, RoutedEventArgs e) // We're writing to a file when the event happens. Add
        {
            var employee = new Employee
            {
                Name = nameText.Text,
                Title = titleText.Text
            };
            Employee.WriteEmployee(employee);
        }
        private void Back_Click(object sender, RoutedEventArgs e) // Back button
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
