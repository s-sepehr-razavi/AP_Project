using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using Utility;

namespace WpfPostManagement.View
{
    /// <summary>
    /// Interaction logic for EmployeePanel.xaml
    /// </summary>
    public partial class EmployeePanel : Window
    {
        Employee employee;
        Customer OrderCustomer;
        public static bool CheckEmpty(string text)
        {
            return string.IsNullOrEmpty(text);
        }
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

            txtCustomerName.Text = "";
            txtCustomerLastName.Text = "";
            txtCustomerSSN.Text = "";
            txtCustomerEmail.Text = "";
            txtCustomerPhoneNUMBER.Text = "";
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Collapsed;
            OrderPanel.Visibility = Visibility.Visible;
            ReportPanel.Visibility = Visibility.Collapsed;
            BoxInformationPanel.Visibility = Visibility.Collapsed;

            SerchSSNPanel.Visibility = Visibility.Visible;
            MainSSNPanel.Visibility = Visibility.Collapsed;
            txtSSNsearch.Text = "";

            txtSenderAddress.Text = "";
            txtReceiverAddress.Text = "";
            ComboPackageContent.SelectedIndex = -1;
            ComboValubleBox.SelectedIndex = -1;
            ComboShipment.SelectedIndex = -1;
            txtPhonenUMBER.Text = "";
            txtWeight.Text = "";
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
            if (!Evaluator.checkName(txtCustomerName.Text))
            {
                MessageBox.Show("Names must be at least 3 letters and at most 32 letters and only consist of letters Do not have characters or numbers.");
            }
            else if (!Evaluator.checkName(txtCustomerLastName.Text))
            {
                MessageBox.Show("Lastnames must be at least 3 letters and at most 32 letters and only consist of letters Do not have characters or numbers.");
            }
            else if (! Evaluator.checkSSN(txtCustomerSSN.Text))
            {
                MessageBox.Show("The national code must be 10 numbers and the first two numbers must be 00.");
            }
            else if (! Evaluator.checkEmail(txtCustomerEmail.Text))
            {
                MessageBox.Show("The email must be in the format A@B.C, where A and B are at least 3 letters and maximum 32 letters each And C can be at least 2 letters and at most 3 letters.");
            }
            else if (! Evaluator.checkPhonenumber(txtCustomerPhoneNUMBER.Text))
            {
                MessageBox.Show("Mobile numbers must be exactly 11 numbers and start with 09.");
            }
            else
            {
                Customer customer = new Customer(txtCustomerName.Text, txtCustomerLastName.Text, txtCustomerEmail.Text, txtCustomerSSN.Text, txtCustomerPhoneNUMBER.Text);

                txtCustomerName.Text = "";
                txtCustomerLastName.Text = "";
                txtCustomerSSN.Text = "";
                txtCustomerEmail.Text = "";
                txtCustomerPhoneNUMBER.Text = "";

                MessageBox.Show("Username: " + customer.username + " Password: " +customer.password);

                RegisterPanel.Visibility = Visibility.Collapsed;
            }

          
        }

        private void btnSsnSearch_Click(object sender, RoutedEventArgs e)
        {
            bool flagMain = false;
            

            for (int i = 0; i < Customer.customers.Count; i++)
            {
                if (txtSSNsearch.Text == Customer.customers[i].id)
                {
                    flagMain = true;
                    OrderCustomer = Customer.customers[i];
                    break;
                }
            }

            if (flagMain)
            {
                SerchSSNPanel.Visibility = Visibility.Collapsed;
                MainSSNPanel.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("This SSN doesn't exist.");

                OrderPanel.Visibility = Visibility.Collapsed;
                
                RegisterPanel.Visibility = Visibility.Visible;

                txtCustomerName.Text = "";
                txtCustomerLastName.Text = "";
                txtCustomerSSN.Text = "";
                txtCustomerEmail.Text = "";
                txtCustomerPhoneNUMBER.Text = "";

                txtSSNsearch.Text = "";
            }

        }

        private void btnOrderSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckEmpty(txtSenderAddress.Text))
            {
                MessageBox.Show("Sender address cannot empty.");
            }
            else if (CheckEmpty(txtReceiverAddress.Text))
            {
                MessageBox.Show("Receiver address cannot empty.");
            }
            else if (ComboPackageContent.SelectedIndex < 0)
            {
                MessageBox.Show("Package Content => An option must be selected.");
            }
            else if (ComboValubleBox.SelectedIndex < 0)
            {
                MessageBox.Show("Valuble Item => An option must be selected.");
            }
            else if (ComboShipment.SelectedIndex < 0)
            {
                MessageBox.Show("Shipment Type => An option must be selected.");
            }
            else if (! Evaluator.checkPhonenumberOrder(txtPhonenUMBER.Text))
            {
                MessageBox.Show("--Empty-- Or Mobile numbers must be exactly 11 numbers and start with 09.");
            }
            else if (! double.TryParse(txtWeight.Text , out double weight))
            {
                MessageBox.Show("The entered field must be a number.");
            }
            else
            {
                bool Expensive;
                if (ComboValubleBox.SelectedIndex == 0)
                {
                    Expensive = true;
                }
                else
                {
                    Expensive = false;
                }


                bool ShipmentType;
                if (ComboShipment.SelectedIndex == 0)
                {
                    ShipmentType = false;
                }
                else
                {
                    ShipmentType = true;
                }

                Content PackageContent = (Content)ComboPackageContent.SelectedIndex;

                double Weight = double.Parse(txtWeight.Text);


                Post AddPost = new Post (txtReceiverAddress.Text,txtSenderAddress.Text, PackageContent, Expensive, weight, txtPhonenUMBER.Text , ShipmentType, txtSSNsearch.Text);
                MessageBox.Show("BOX ID:" + AddPost.id.ToString());
            }
        }

        private void OrderPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnShowPrice_Click(object sender, RoutedEventArgs e)
        {
            if (CheckEmpty(txtSenderAddress.Text))
            {
                MessageBox.Show("Sender address cannot empty.");
            }
            else if (CheckEmpty(txtReceiverAddress.Text))
            {
                MessageBox.Show("Receiver address cannot empty.");
            }
            else if (ComboPackageContent.SelectedIndex < 0)
            {
                MessageBox.Show("Package Content => An option must be selected.");
            }
            else if (ComboValubleBox.SelectedIndex < 0)
            {
                MessageBox.Show("Valuble Item => An option must be selected.");
            }
            else if (ComboShipment.SelectedIndex < 0)
            {
                MessageBox.Show("Shipment Type => An option must be selected.");
            }
            else if (!Evaluator.checkPhonenumberOrder(txtPhonenUMBER.Text))
            {
                MessageBox.Show("--Empty-- Or Mobile numbers must be exactly 11 numbers and start with 09.");
            }
            else if (!double.TryParse(txtWeight.Text, out double weight))
            {
                MessageBox.Show("The entered field must be a number.");
            }
            else
            {
                double price = 10000;

                bool Expensive;
                if (ComboValubleBox.SelectedIndex == 0)
                {
                    Expensive = true;
                }
                else
                {
                    Expensive = false;
                }


                bool ShipmentType;
                if (ComboShipment.SelectedIndex == 0)
                {
                    ShipmentType = false;
                }
                else
                {
                    ShipmentType = true;
                }


                Content PackageContent = (Content)ComboPackageContent.SelectedIndex;

                double Weight = double.Parse(txtWeight.Text);

                if (PackageContent == DataAccess.Content.Document)
                {
                    price *= 1.5;
                }
                else if (PackageContent == DataAccess.Content.Fragile)
                {
                    price *= 2;
                }

                if (Expensive)
                {
                    price *= 2;
                }

                if (ShipmentType)
                {
                    price *= 1.5;
                }

                if (Weight > 0.5)
                {
                    int Coefficient = (int)(weight / 0.5);
                    if (weight % 0.5 == 0)
                    {
                        Coefficient -= 1;
                    }
                    price *= Math.Pow(1.2, Coefficient);
                }

                string strPrice = (price).ToString();

                LableShowPrice.Content = strPrice;

                MainSSNPanel.Visibility = Visibility.Collapsed;
                ShowPricePanel.Visibility = Visibility.Visible;
            }
        }

        private void btnBackShowPrice_Click(object sender, RoutedEventArgs e)
        {
            MainSSNPanel.Visibility = Visibility.Visible;
            ShowPricePanel.Visibility = Visibility.Collapsed;
        }

        private void BtnSearchRepot_Click(object sender, RoutedEventArgs e)
        {
            int checkCounter = 0;
            //1
            bool ssnFlag = false;
            string ssn = null;
            if (! CheckEmpty(txtSearchBaseSSN.Text))
            {
                ssnFlag = true;
                ssn = txtSearchBaseSSN.Text;
                checkCounter++;
            }
            //2
            bool PackageContentFlag = false;
            DataAccess.Content ? PackageContent = null;
            if (CombSearchBasePackageContent.SelectedIndex > -1)
            {
                PackageContentFlag = true;
                PackageContent = (Content)CombSearchBasePackageContent.SelectedIndex;
                checkCounter++;
            }
            //3
            bool PriceFlag = false;
            string Price = null; 
            if (! CheckEmpty(txtSearchBasePrice.Text))
            {
                PriceFlag = true;
                Price = txtSearchBasePrice.Text;
                checkCounter++;
            }
            //4
            bool ShipmentFlag = false;
            bool Shipment = false;
            if (ComboSearchBaseShipment.SelectedIndex > -1)
            {
                checkCounter++;
                if (ComboSearchBaseShipment.SelectedIndex == 0)
                {
                    Shipment = false;
                }
                else
                {
                    Shipment = true;
                }
            }
            //5
            bool WeightFlag = false;
            string Weight = null;
            if (! CheckEmpty(txtSearchBaseWeight.Text))
            {
                checkCounter++;
                WeightFlag = true;
                Weight = txtSearchBaseWeight.Text;
            }

            List<Post> tempPost = new List<Post>();

            bool GlobalFlag = false;

            for (int i=0; i < Post.posts.Count; i++)
            {
                int Count = 0;
                //1
                if (ssnFlag)
                {
                    if (Post.posts[i].SSN == ssn)
                    {
                        Count++;
                    }
                }

                //2
                if (PackageContentFlag)
                {
                    if (Enum.Equals(Post.posts[i].content, PackageContent))
                    {
                        Count++;
                    }
                }
                //3
                if (PriceFlag)
                {
                    if (Post.posts[i].price == Price)
                    {
                        Count++;
                    }
                }
                //4
                if (ShipmentFlag)
                {
                    if (Post.posts[i].express == Shipment)
                    {
                        Count++;
                    }
                }
                //5
                if (WeightFlag)
                {
                    if (Post.posts[i].weight.ToString() == Weight)
                    {
                        Count++;
                    }
                }

                if (Count == checkCounter)
                {
                    GlobalFlag = true;
                    tempPost.Add(Post.posts[i]);
                }

            }

            if (!GlobalFlag)
            {
                MessageBox.Show("No order has been registered with this information.");
            }
        }
    }

}
