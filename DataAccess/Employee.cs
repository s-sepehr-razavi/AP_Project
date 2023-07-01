using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Employee
    {
        public string name { set; get; }
        public string lastName { set; get; }
        public string id { set; get; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public static List<Employee> EmployeesList { get; set; } = new List<Employee>();

        public Employee(string name, string lastName, string id, string username, string email, string password)
        {
            this.name = name;
            this.lastName = lastName;
            this.id = id;
            this.username = username;
            this.email = email;
            this.password = password;
            EmployeesList.Add(this);
        }

        public Employee() { }
    }
}