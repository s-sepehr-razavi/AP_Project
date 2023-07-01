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
using static System.Net.Mime.MediaTypeNames;

namespace WpfPostManagement.View
{
    /// <summary>
    /// Interaction logic for CustomerPanel.xaml
    /// </summary>
    public partial class CustomerPanel : Window
    {
        Customer customer;
        Post PostBoxInformation;
        public CustomerPanel (Customer cus)
        {
            this.customer = cus;
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
            System.Windows.Application.Current.Shutdown();
        }
        public static bool CheckEmpty(string text)
        {
            return string.IsNullOrEmpty(text);
        }
        public static bool CheckInt (string text)
        {
            if (int.TryParse(text ,out int number))
            {
                if (number > 0)
                {
                    return true;
                }
            }

            return false;
        }
        public static bool ChackDouble(string text)
        {
            if (double.TryParse(text, out double number))
            {
                if (number > 0)
                {
                    return true;
                }
            }

            return false;
        }
        private void btnReportCustomer_Click(object sender, RoutedEventArgs e)
        {
            ReportCustomerPanel.Visibility = Visibility.Visible;
            BoxInformationPanelCustomer.Visibility = Visibility.Collapsed;
            WalletPanel.Visibility = Visibility.Collapsed;  
            ChangeInfoPanle.Visibility= Visibility.Collapsed;

            CombSearchBasePackageContentCustomer.SelectedIndex = -1;
            txtSearchBasePriceCustomer.Text = "";
            ComboSearchBaseShipmentCustomer.SelectedIndex = -1;
            txtSearchBaseWeightCustomer.Text = "";
        }

        private void btnInformationCustomer_Click(object sender, RoutedEventArgs e)
        {
            ReportCustomerPanel.Visibility = Visibility.Collapsed;
            BoxInformationPanelCustomer.Visibility = Visibility.Visible;
            WalletPanel.Visibility = Visibility.Collapsed;
            ChangeInfoPanle.Visibility = Visibility.Collapsed;

            MainBoxInformationPanelCustomer.Visibility = Visibility.Collapsed;
            SearchInformatioPanelCustomer.Visibility = Visibility.Visible;
            txtIdSearchCustomer.Text = "";
        }

        private void btnWallet_Click(object sender, RoutedEventArgs e)
        {
            ReportCustomerPanel.Visibility = Visibility.Collapsed;
            BoxInformationPanelCustomer.Visibility = Visibility.Collapsed;
            WalletPanel.Visibility = Visibility.Visible;
            ChangeInfoPanle.Visibility = Visibility.Collapsed;

            AccountBalance.Content = customer.AccountBalance.ToString();
            MainWalletPanel.Visibility = Visibility.Visible;
            ChargePanel.Visibility = Visibility.Collapsed;
        }

        private void btnChangeInfo_Click(object sender, RoutedEventArgs e)
        {
            ReportCustomerPanel.Visibility = Visibility.Collapsed;
            BoxInformationPanelCustomer.Visibility = Visibility.Collapsed;
            WalletPanel.Visibility = Visibility.Collapsed;
            ChangeInfoPanle.Visibility = Visibility.Visible;

            txtNewUserName.Text = "";
            txtNewPassword.Text = "";
        }

        private void btnBackCustomer_Click(object sender, RoutedEventArgs e)
        {
            LogInView Test = new LogInView();
            Test.Show();
            this.Close();
        }

        private void BtnSearchRepotCustomer_Click(object sender, RoutedEventArgs e)
        {
            int checkCounter = 0;
            
            //1
            bool PackageContentFlag = false;
            DataAccess.Content? PackageContent = null;
            if (CombSearchBasePackageContentCustomer.SelectedIndex > -1)
            {
                PackageContentFlag = true;
                PackageContent = (Content)CombSearchBasePackageContentCustomer.SelectedIndex;
                checkCounter++;
            }
            //2
            bool PriceFlag = false;
            string Price = null;
            if (!CheckEmpty(txtSearchBasePriceCustomer.Text))
            {
                PriceFlag = true;
                Price = txtSearchBasePriceCustomer.Text;
                checkCounter++;
            }
            //3
            bool ShipmentFlag = false;
            bool Shipment = false;
            if (ComboSearchBaseShipmentCustomer.SelectedIndex > -1)
            {
                checkCounter++;
                ShipmentFlag = true;
                if (ComboSearchBaseShipmentCustomer.SelectedIndex == 0)
                {
                    Shipment = false;
                }
                else
                {
                    Shipment = true;
                }
            }
            //4
            bool WeightFlag = false;
            string Weight = null;
            if (!CheckEmpty(txtSearchBaseWeightCustomer.Text))
            {
                checkCounter++;
                WeightFlag = true;
                Weight = txtSearchBaseWeightCustomer.Text;
            }

            List<Post> tempPost = new List<Post>();

            bool GlobalFlag = false;

            for (int i = 0; i < Post.posts.Count; i++)
            {
                int Count = 0;

                //1
                if (PackageContentFlag)
                {
                    if (Post.posts[i].content == PackageContent)
                    {
                        Count++;
                    }
                }
                //2
                if (PriceFlag)
                {
                    if (Post.posts[i].price == Price)
                    {
                        Count++;
                    }
                }
                //3
                if (ShipmentFlag)
                {
                    if (Post.posts[i].express.ToString() == Shipment.ToString())
                    {
                        Count++;
                    }
                }
                //4
                if (WeightFlag)
                {
                    if (Post.posts[i].weight.ToString() == Weight)
                    {
                        Count++;
                    }
                }

                if (Count == checkCounter)
                {
                    tempPost.Add(Post.posts[i]);
                }

            }

            List<Post> FinalPost = new List<Post>();

            for (int  i = 0; i < tempPost.Count; i++)
            {
                if (tempPost[i].SSN == customer.id)
                {
                    GlobalFlag = true;
                    FinalPost.Add(tempPost[i]);
                }
            }

            if (!GlobalFlag)
            {
                MessageBox.Show("No order has been registered with this information.");
            }
            else
            {
                Function.customerReport(FinalPost, customer.username);
            }

            CombSearchBasePackageContentCustomer.SelectedIndex = -1;
            txtSearchBasePriceCustomer.Text = "";
            ComboSearchBaseShipmentCustomer.SelectedIndex = -1;
            txtSearchBaseWeightCustomer.Text = "";

        }

        private void btnBoxSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;

            for (int i = 0; i < Post.posts.Count(); i++)
            {
                if (Post.posts[i].id.ToString() == txtIdSearchCustomer.Text)
                {
                    flag = true;
                    PostBoxInformation = Post.posts[i];
                    break;
                }
            }

            if (flag)
            {
                bool Owner = false;

                if (PostBoxInformation.SSN == customer.id)
                {
                    Owner = true;
                }

                if (Owner)
                {
                    SearchInformatioPanelCustomer.Visibility = Visibility.Collapsed;
                    MainBoxInformationPanelCustomer.Visibility = Visibility.Visible;

                    LableSSNCustomer.Content = PostBoxInformation.SSN;
                    LableSenderAddressCustomer.Content = PostBoxInformation.senderAddress;
                    LableReceiverAddressCustomer.Content = PostBoxInformation.recieverAddress;
                    LablePackgeContentCustomer.Content = PostBoxInformation.content.ToString();

                    string valuable = "";
                    if (PostBoxInformation.expensive)
                    {
                        valuable = "Yes";
                    }
                    else
                    {
                        valuable = "No";
                    }
                    LableValubleItemCustomer.Content = valuable;

                    string shipmnet = "";
                    if (PostBoxInformation.express)
                    {
                        shipmnet = "Express";
                    }
                    else
                    {
                        shipmnet = "Regular";
                    }

                    LableShipmentTypeCustomer.Content = shipmnet;

                    LablePhoneNumberCustomer.Content = PostBoxInformation.phonenumber;

                    LableWeightCustomer.Content = PostBoxInformation.weight;

                    LableTotalPriceCustomer.Content = PostBoxInformation.price;

                    LablePackageIdCustomer.Content = PostBoxInformation.id;

                    LableStatusBoxInformation.Content = PostBoxInformation.PostStaus.ToString();
                }

                else
                {
                    MessageBox.Show("This box does not belong to this customer.");
                }
            }
            else
            {
                MessageBox.Show("This ID doesn't exist");
            }

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (PostBoxInformation.PostStaus == Status.Deliverd)
            {
                CustomerOpinionStackPanel.Visibility = Visibility.Visible;
                MainBoxInformationPanelCustomer.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("You do not have access to this section until the status of the package changes to Delivered.");
            }    
        }

        private void btnBackCustumerOpinion_Click(object sender, RoutedEventArgs e)
        {
            PostBoxInformation.CustomerOpinion = txtCustomerId.Text;

            CustomerOpinionStackPanel.Visibility = Visibility.Collapsed;
            MainBoxInformationPanelCustomer.Visibility = Visibility.Visible;
        }

        private void btnChargeAccount_Click(object sender, RoutedEventArgs e)
        {
            MainWalletPanel.Visibility = Visibility.Collapsed;

            txtCardNumber.Text = "";
            txtCVV2.Text = "";
            txtYear.Text = "";
            txtMonth.Text = "";
            txtDay.Text = "";
            txtAmountOfMoney.Text = "";
            ReciptCombo.SelectedIndex = -1;

            ChargePanel.Visibility = Visibility.Visible;
        }

        private void btnOkChangeInfo_Click(object sender, RoutedEventArgs e)
        {
            bool ReUsername = false;
            for (int i = 0; i < Customer.customers.Count; i++)
            {
                if (txtNewUserName.Text == Customer.customers[i].username)
                {
                    ReUsername = true;
                }
            }

            for (int i = 0; i < Employee.EmployeesList.Count; i++)
            {
                if (Employee.EmployeesList[i].username == txtNewUserName.Text)
                {
                    ReUsername = true;
                }
            } 

            if (ReUsername)
            {
                MessageBox.Show("This username already exist.");
            }
            else if (!CheckEmpty(txtNewUserName.Text))
            {
                customer.username = txtNewUserName.Text;
                if (!CheckEmpty(txtNewPassword.Text))
                {
                    customer.password = txtNewPassword.Text;
                }
                else
                {
                    MessageBox.Show("If password box is empty,then password is not changed.");
                }
            }
            else
            {
                MessageBox.Show("If username box is empty,then username is not changed.");
            }

            

            ChangeInfoPanle.Visibility = Visibility.Collapsed;

        }

        private void btnOkBoxInformation_Click(object sender, RoutedEventArgs e)
        {
            BoxInformationPanelCustomer.Visibility = Visibility.Collapsed;
            MainBoxInformationPanelCustomer.Visibility= Visibility.Collapsed;
            SearchInformatioPanelCustomer.Visibility = Visibility.Collapsed;
        }

        private void btnOkCard_Click(object sender, RoutedEventArgs e)
        {
            if (!Evaluator.validateLuhnAlgorithm(txtCardNumber.Text))
            {
                MessageBox.Show("Invalid pattern card.");
            }
            else if (! Evaluator.checkCVV(txtCVV2.Text))
            {
                MessageBox.Show("Invalid CVV2 pattern.");
            }
            else if (! CheckInt(txtYear.Text))
            {
                MessageBox.Show("Year => Must be a positive number.");
            }
            else if ((1 > int.Parse(txtYear.Text))|| (int.Parse(txtYear.Text) > 9999) )
            {
                MessageBox.Show("Year is between 1 to 9999.");
            }
            else if (! CheckInt(txtMonth.Text))
            {
                MessageBox.Show("Month => Must be a positive number.");
            }
            else if ((1 > int.Parse(txtMonth.Text)) || (int.Parse(txtMonth.Text) > 12))
            {
                MessageBox.Show("Month is between 1 to 12.");
            }
            else if (! Evaluator.checkDate(int.Parse(txtYear.Text), int.Parse( txtMonth.Text )))
            {
                MessageBox.Show("The card has expired.");
            }
            else if (!ChackDouble(txtAmountOfMoney.Text))
            {
                MessageBox.Show("Amout must be a positive value.");
            }
            else if (ReciptCombo.SelectedIndex == -1)
            {
                MessageBox.Show("The receipt request option must be filled.");
            }
            else
            {
                customer.AccountBalance += double.Parse(txtAmountOfMoney.Text);

                //Yes
                if (ReciptCombo.SelectedIndex == 0)
                {
                    string input = "Charge Amount: " + txtAmountOfMoney.Text + ",Account Balance: " + customer.AccountBalance + "Time: " + DateTime.Now.ToString();
                    Function.printReceipt(input, DateTime.Now.ToString() + " " + customer.id);
                }

                WalletPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
