using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class Customer
    {
        public string name { set; get; }
        public string id { set; get; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phonenumber { get;set; }
        static public List<Customer> customers { get; set; } = new List<Customer>();
        public List<Post> posts { get; set; } = new List<Post>();

        public Customer(string name, string email, string id, string phonenumber)
        {
            this.name = name;            
            this.email = email;
            this.id = id;
            this.phonenumber = phonenumber;

            Random random = new Random();
            while (true)
            {
                int rand = random.Next(0) % 100000000;
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
                int rand = random.Next(0) % 10000;
                string r = rand.ToString();
                for (int i = 0; i < 4 - r.Length; i++)
                {
                    r = 0 + r;
                }

                bool flag = true;
                foreach (var item in customers)
                {
                    if (item.username == r)
                    {
                        flag = false; break;
                    }
                }


                if (flag)
                {
                    username = r;
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
