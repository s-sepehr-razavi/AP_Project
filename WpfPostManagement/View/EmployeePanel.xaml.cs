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

namespace WpfPostManagement.View
{
    /// <summary>
    /// Interaction logic for EmployeePanel.xaml
    /// </summary>
    public partial class EmployeePanel : Window
    {
        Employee employee;
        public EmployeePanel(Employee emp)
        {
            this.employee = emp;
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

        private void btnRgesterCustomer_Click(object sender, RoutedEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Visible;
            OrderPanel.Visibility = Visibility.Collapsed;
            ReportPanel.Visibility = Visibility.Collapsed;  
            BoxInformationPanel.Visibility = Visibility.Collapsed;
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Collapsed;
            OrderPanel.Visibility = Visibility.Visible;
            ReportPanel.Visibility = Visibility.Collapsed;
            BoxInformationPanel.Visibility = Visibility.Collapsed;
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Collapsed;
            OrderPanel.Visibility = Visibility.Collapsed;
            ReportPanel.Visibility = Visibility.Visible;
            BoxInformationPanel.Visibility = Visibility.Collapsed;
        }

        private void btnInformation_Click(object sender, RoutedEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Collapsed;
            OrderPanel.Visibility = Visibility.Collapsed;
            ReportPanel.Visibility = Visibility.Collapsed;
            BoxInformationPanel.Visibility = Visibility.Visible;
        }      

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LogInView Test = new LogInView();
            Test.Show();
            this.Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSsnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOrderSubmit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrderPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnShowPrice_Click(object sender, RoutedEventArgs e)
        {
            MainSSNPanel.Visibility = Visibility.Collapsed;
            ShowPricePanel.Visibility = Visibility.Visible;
        }

        private void btnBackShowPrice_Click(object sender, RoutedEventArgs e)
        {
            MainSSNPanel.Visibility = Visibility.Visible;
            ShowPricePanel.Visibility = Visibility.Collapsed;
        }
    }

}
