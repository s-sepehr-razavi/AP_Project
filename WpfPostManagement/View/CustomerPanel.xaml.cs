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
    /// Interaction logic for CustomerPanel.xaml
    /// </summary>
    public partial class CustomerPanel : Window
    {
        public CustomerPanel()
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

        private void btnReportCustomer_Click(object sender, RoutedEventArgs e)
        {
            ReportCustomerPanel.Visibility = Visibility.Visible;
            BoxInformationPanelCustomer.Visibility = Visibility.Collapsed;
            WalletPanel.Visibility = Visibility.Collapsed;  
            ChangeInfoPanle.Visibility= Visibility.Collapsed;
        }

        private void btnInformationCustomer_Click(object sender, RoutedEventArgs e)
        {
            ReportCustomerPanel.Visibility = Visibility.Collapsed;
            BoxInformationPanelCustomer.Visibility = Visibility.Visible;
            WalletPanel.Visibility = Visibility.Collapsed;
            ChangeInfoPanle.Visibility = Visibility.Collapsed;
        }

        private void btnWallet_Click(object sender, RoutedEventArgs e)
        {
            ReportCustomerPanel.Visibility = Visibility.Collapsed;
            BoxInformationPanelCustomer.Visibility = Visibility.Collapsed;
            WalletPanel.Visibility = Visibility.Visible;
            ChangeInfoPanle.Visibility = Visibility.Collapsed;
        }

        private void btnChangeInfo_Click(object sender, RoutedEventArgs e)
        {
            ReportCustomerPanel.Visibility = Visibility.Collapsed;
            BoxInformationPanelCustomer.Visibility = Visibility.Collapsed;
            WalletPanel.Visibility = Visibility.Collapsed;
            ChangeInfoPanle.Visibility = Visibility.Visible;
        }

        private void btnBackCustomer_Click(object sender, RoutedEventArgs e)
        {
            LogInView Test = new LogInView();
            Test.Show();
            this.Close();
        }

        private void BtnSearchRepotCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBoxSearchCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            CustomerOpinionStackPanel.Visibility = Visibility.Visible;
            MainBoxInformationPanelCustomer.Visibility = Visibility.Collapsed;
        }

        private void btnBackCustumerOpinion_Click(object sender, RoutedEventArgs e)
        {
            CustomerOpinionStackPanel.Visibility = Visibility.Collapsed;
            MainBoxInformationPanelCustomer.Visibility = Visibility.Visible;
        }

        private void btnChargeAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOkChangeInfo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
