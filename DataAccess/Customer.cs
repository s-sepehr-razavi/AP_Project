using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Customer
    {
        public string name { set; get; }
        public string lastName { set; get; }
        public string id { set; get; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phonenumber { get;set; }
        public static  List<Customer> customers { get; set; } = new List<Customer>();
        public double AccountBalance;

        public Customer(string name, string lastName ,string email, string id, string phonenumber)
        {
            this.name = name;    
            this.lastName = lastName;
            this.email = email;
            this.id = id;
            this.phonenumber = phonenumber;
            this.AccountBalance = 0;
            

            Random randomPass = new Random();
            string password = randomPass.Next(10000000, 100000000).ToString();
            this.password = password;

            Random random = new Random();
            while (true)
            {
                int rand = random.Next(0,10000);
                bool flag = true;

                foreach (var item in customers)
                {
                    if (item.username == "user" + rand.ToString())
                    {
                        flag = false; break;
                    }
                }

                if (flag)
                {
                    this.username = "user" + rand.ToString();
                    break;
                }
            }

            customers.Add(this);
        }

        static public Customer findCustomer(string id)
        {
            foreach (var item in customers)
            {
                if (item.id == id)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
