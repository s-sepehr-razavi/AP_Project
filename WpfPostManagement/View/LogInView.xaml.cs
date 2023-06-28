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

namespace WpfPostManagement.View
{
    /// <summary>
    /// Interaction logic for LogInView.xaml
    /// </summary>
    public partial class LogInView : Window
    {
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
            //Employee Panel

            //EmployeePanel employeePanel = new EmployeePanel();
            //employeePanel.Show();

            //Customer Panel
            CustomerPanel customerPanel= new CustomerPanel();
            customerPanel.Show();

            this.Close();
        }

        private void btnRegisterEmployees_Click(object sender, RoutedEventArgs e)
        {
            
            RegisterEmployees register = new RegisterEmployees();
            register.Show();
            this.Close();
        }
    }
}
