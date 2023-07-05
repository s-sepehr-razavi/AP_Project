using DataAccess;
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
using Utility;

namespace WpfPostManagement.View
{
    /// <summary>
    /// Interaction logic for RegisterEmployees.xaml
    /// </summary>
    public partial class RegisterEmployees : Window
    {
        public RegisterEmployees()
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
            Function.SavingDatatoDB();
            Application.Current.Shutdown();
        }

        
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LogInView Test = new LogInView();
            Test.Show();
            this.Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool ReUsername = false;
            for (int i = 0; i < Customer.customers.Count; i++)
            {
                if (txtUsername.Text == Customer.customers[i].username)
                {
                    ReUsername = true;
                }
            }

            for (int i = 0; i < Employee.EmployeesList.Count; i++)
            {
                if (Employee.EmployeesList[i].username == txtUsername.Text)
                {
                    ReUsername = true;
                }
            }

            bool ReId = false;

            for (int i = 0; i < Employee.EmployeesList.Count; i++)
            {
                if (Employee.EmployeesList[i].id == txtid.Text)
                {
                    ReId = true;
                }
            }

            if (! Evaluator.checkName(txtEmployeeName.Text))
            {
                MessageBox.Show("Names must be at least 3 letters and at most 32 letters and only consist of letters Do not have characters or numbers.");
            }
            else if (!Evaluator.checkName(txtLastName.Text))
            {
                MessageBox.Show("Lastnames must be at least 3 letters and at most 32 letters and only consist of letters Do not have characters or numbers.");
            }
            else if (!Evaluator.checkEmployeeID(txtid.Text))
            {
                MessageBox.Show("The personnel code of the employee during registration should contain only 5 numbers and the third number should be 9.");
            }
            else if (ReId)
            {
                MessageBox.Show("This ID already exist.");
            }
            else if (! Evaluator.checkName(txtUsername.Text))
            {
                MessageBox.Show("Usernames must be at least 3 letters and at most 32 letters and only consist of letters Do not have characters or numbers.");
            }
            else if (ReUsername)
            {
                MessageBox.Show("This username already taken.");
            }
            else if (!Evaluator.checkEmail(txtEmail.Text))
            {
                MessageBox.Show("The email must be in the format A@B.C, where A and B are at least 3 letters and maximum 32 letters each And C can be at least 2 letters and at most 3 letters.");
            }
            else if (!Evaluator.checkCustomerPassword(txtPass.Text))
            {
                MessageBox.Show("The password entered by the employee must be at least 8 and at most 32 characters.It should also contain at least one uppercase letter, one lowercase letter and one number.");
            }
            else if (txtPass.Text == txtRePass.Text)
            {
                Employee Register = new Employee(txtEmployeeName.Text,txtLastName.Text,txtid.Text,txtUsername.Text,txtEmail.Text,txtPass.Text);
                /*Function.SavingDatatoDB();*/
                LogInView Test = new LogInView();
                Test.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Passwords do not match.");
            }
        }
    }
}
