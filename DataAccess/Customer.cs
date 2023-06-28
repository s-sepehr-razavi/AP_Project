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
        public string id { set; get; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phonenumber { get;set; }
        public static  List<Customer> customers { get; set; } = new List<Customer>();

        public Customer(string name, string email, string id, string phonenumber)
        {
            this.name = name;            
            this.email = email;
            this.id = id;
            this.phonenumber = phonenumber;

            Random random = new Random();
            while (true)
            {
                int rand = random.Next(0);
                bool flag = true;
                foreach (var item in customers)
                {
                    if (item.password == rand.ToString())
                    {
                        flag = false; break;
                    }
                }

                if (flag)
                {
                    password = rand.ToString();
                    break;
                }
            }

            while (true)
            {
                int rand = random.Next(0);
                bool flag = true;
                foreach (var item in customers)
                {
                    if (item.username == rand.ToString())
                    {
                        flag = false; break;
                    }
                }

                if (flag)
                {
                    username = rand.ToString();
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
