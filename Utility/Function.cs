using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Net.Mail;
using System.Net;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SQLite;

namespace Utility
{
    public static class Function
    {
        private const string connectionString = "Data Source=mydatabase.db;Version=3;";

        static public void employeeReport(List<Post> result, string name)
        {
            string filePath = name + "EmployeeReport.csv";

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(result);
            }
        }

        static public void customerReport(List<Post> result, string name)
        {
            string filePath = name + "CustomerReport.csv";

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(result);
            }
        }

        public static void SendEmail(string email, string input, string subject)
        {
            string senderEmail = "s.sepehr.razavi@gmail.com"; // enter sender email
            string senderPassword = ""; // enter sender password        
            string body = input;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(senderEmail);
            mail.To.Add(new MailAddress(email));
            mail.Subject = subject;
            mail.Body = body;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
            }
        }

        public static void printReceipt(string input, string name)
        {
            using (FileStream fileStream = new FileStream(name + "Receipt.pdf", FileMode.Create))
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, fileStream);

                document.Open();
                document.Add(new Paragraph(input));
                document.Close();
            }
        }



        static public void CreateTables()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Create Table 1: Customers
                string createCustomersTableSql = "CREATE TABLE IF NOT EXISTS Customers (Name TEXT, LastName TEXT, Id TEXT PRIMARY KEY, Username TEXT, Email TEXT, Password TEXT)";
                using (SQLiteCommand createCustomersTableCommand = new SQLiteCommand(createCustomersTableSql, connection))
                {
                    createCustomersTableCommand.ExecuteNonQuery();
                }

                // Create Table 2: Employees
                string createEmployeesTableSql = "CREATE TABLE IF NOT EXISTS Employees (Name TEXT, LastName TEXT, Id TEXT PRIMARY KEY, Username TEXT, Email TEXT, Password TEXT)";
                using (SQLiteCommand createEmployeesTableCommand = new SQLiteCommand(createEmployeesTableSql, connection))
                {
                    createEmployeesTableCommand.ExecuteNonQuery();
                }

                // Create Table 3: Posts
                string createPostsTableSql = "CREATE TABLE IF NOT EXISTS Posts (RecieverAddress TEXT, SenderAddress TEXT, Content TEXT, Expensive INTEGER, Weight REAL, PhoneNumber TEXT, Express INTEGER, ID INTEGER PRIMARY KEY AUTOINCREMENT, SenderID TEXT, SSN TEXT, Price TEXT, CustomerOpinion TEXT, PostStatus TEXT)";
                using (SQLiteCommand createPostsTableCommand = new SQLiteCommand(createPostsTableSql, connection))
                {
                    createPostsTableCommand.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        static public void InsertCustomer(Customer customer)
        {
            if (CustomerExists(customer.id))
            {
                return;
            }

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Customers (Name, LastName, Id, Username, Email, Password) VALUES (@Name, @LastName, @Id, @Username, @Email, @Password)";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", customer.name);
                    command.Parameters.AddWithValue("@LastName", customer.lastName);
                    command.Parameters.AddWithValue("@Id", customer.id);
                    command.Parameters.AddWithValue("@Username", customer.username);
                    command.Parameters.AddWithValue("@Email", customer.email);
                    command.Parameters.AddWithValue("@Password", customer.password);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        static public void InsertEmployee(Employee employee)
        {
            if (EmployeeExists(employee.id))
            {
                return;
            }
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Employees (Name, LastName, Id, Username, Email, Password) VALUES (@Name, @LastName, @Id, @Username, @Email, @Password)";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", employee.name);
                    command.Parameters.AddWithValue("@LastName", employee.lastName);
                    command.Parameters.AddWithValue("@Id", employee.id);
                    command.Parameters.AddWithValue("@Username", employee.username);
                    command.Parameters.AddWithValue("@Email", employee.email);
                    command.Parameters.AddWithValue("@Password", employee.password);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        static public List<Customer> RetrieveCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Customers";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer();
                            customer.name = reader.GetString(0);
                            customer.lastName = reader.GetString(1);
                            customer.id = reader.GetString(2);
                            customer.username = reader.GetString(3);
                            customer.email = reader.GetString(4);
                            customer.password = reader.GetString(5);

                            customers.Add(customer);
                        }
                    }
                }

                connection.Close();
            }

            return customers;
        }

        static public List<Employee> RetrieveEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Employees";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee();
                            employee.name = reader.GetString(0);
                            employee.lastName = reader.GetString(1);
                            employee.id = reader.GetString(2);
                            employee.username = reader.GetString(3);
                            employee.email = reader.GetString(4);
                            employee.password = reader.GetString(5);

                            employees.Add(employee);
                        }
                    }
                }

                connection.Close();
            }

            return employees;
        }

        static private bool CustomerExists(string postId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM Customers WHERE ID = @ID";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", postId);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        static private bool EmployeeExists(string postId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM Employees WHERE ID = @ID";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", postId);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        static public void InsertPost(Post post)
        {

            if (PostExists(post.id))
            {
                return;
            }

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Posts (RecieverAddress, SenderAddress, Content, Expensive, Weight, PhoneNumber, Express, ID, SenderID, SSN, Price, CustomerOpinion, PostStatus) VALUES (@RecieverAddress, @SenderAddress, @Content, @Expensive, @Weight, @PhoneNumber, @Express, @ID, @SenderID, @SSN, @Price, @CustomerOpinion, @PostStatus)";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@RecieverAddress", post.recieverAddress);
                    command.Parameters.AddWithValue("@SenderAddress", post.senderAddress);
                    command.Parameters.AddWithValue("@Content", post.content.ToString());
                    command.Parameters.AddWithValue("@Expensive", post.expensive ? 1 : 0);
                    command.Parameters.AddWithValue("@Weight", post.weight);
                    command.Parameters.AddWithValue("@PhoneNumber", post.phonenumber);
                    command.Parameters.AddWithValue("@Express", post.express ? 1 : 0);
                    command.Parameters.AddWithValue("@ID", post.id);
                    command.Parameters.AddWithValue("@SenderID", post.senderID);
                    command.Parameters.AddWithValue("@SSN", post.SSN);
                    command.Parameters.AddWithValue("@Price", post.price);
                    command.Parameters.AddWithValue("@CustomerOpinion", post.CustomerOpinion);
                    command.Parameters.AddWithValue("@PostStatus", post.PostStaus.ToString());

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        static public List<Post> RetrievePosts()
        {
            List<Post> posts = new List<Post>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Posts";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Post post = new Post();
                            post.recieverAddress = reader.GetString(0);
                            post.senderAddress = reader.GetString(1);
                            post.content = (Content)Enum.Parse(typeof(Content), reader.GetString(2));
                            post.expensive = reader.GetInt32(3) == 1;
                            post.weight = reader.GetDouble(4);
                            post.phonenumber = reader.GetString(5);
                            post.express = reader.GetInt32(6) == 1;
                            post.id = reader.GetInt32(7);
                            post.senderID = reader.GetString(8);
                            post.SSN = reader.GetString(9);
                            post.price = reader.GetString(10);
                            post.CustomerOpinion = reader.GetString(11);
                            post.PostStaus = (Status)Enum.Parse(typeof(Status), reader.GetString(12));
                            posts.Add(post);
                        }
                    }
                }

                connection.Close();
            }

            return posts;
        }

        static private bool PostExists(int postId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM Posts WHERE ID = @ID";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", postId);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

    }
}
