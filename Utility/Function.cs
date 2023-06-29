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

namespace Utility
{
    public static class Function
    {

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


    }
}
