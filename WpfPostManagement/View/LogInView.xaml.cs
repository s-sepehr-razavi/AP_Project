using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccess;


namespace WpfPostManagement.View
{
    /// <summary>
    /// Interaction logic for LogInView.xaml
    /// </summary>
    public partial class LogInView : Window
    {
        Employee employee;
        Customer customer;
        public LogInView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogIN_Click(object sender, RoutedEventArgs e)
        {
            
            //FindEmployee
            bool EmployeeFlag = false;

            for (int i=0; i<Employee.EmployeesList.Count; i++)
            {
                if (txtUser.Text == Employee.EmployeesList[i].username)
                {
                    EmployeeFlag = true;
                    employee = Employee.EmployeesList[i];
                    break;
                }
            }

            
            //FindCustomer
            bool CustomerFlag = false;

            for (int i = 0; i < Customer.customers.Count; i++)
            {
                if (txtUser.Text == Customer.customers[i].username)
                {
                    CustomerFlag = true;
                    customer = Customer.customers[i];
                    break;
                }
            }


            //ShowWhichPanelOpen
            if (EmployeeFlag)
            {
                if (employee.password == txtPassword.Password)
                {
                    //Employee Panel
                    EmployeePanel employeePanel = new EmployeePanel();
                    employeePanel.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The password is wrong.");
                }
            }
            else if (CustomerFlag)
            {
                if (customer.password == txtPassword.Password)
                {
                    //Customer Panel
                    CustomerPanel customerPanel = new CustomerPanel();
                    customerPanel.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The password is wrong.");
                }
            }
            else
            {
                MessageBox.Show("This username does not match any usernames.");
            }
            
        }

        private void btnRegisterEmployees_Click(object sender, RoutedEventArgs e)
        {
            
            RegisterEmployees register = new RegisterEmployees();
            register.Show();
            this.Close();
        }
    }
}
