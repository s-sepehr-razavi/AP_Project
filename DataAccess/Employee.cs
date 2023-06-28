using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class Employee
    {
        public string name { set; get; }
        public string id { set; get; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public Employee(string name, string id, string username, string email, string password)
        {
            this.name = name;
            this.id = id;
            this.username = username;
            this.email = email;
            this.password = password;
        }
    }
}
