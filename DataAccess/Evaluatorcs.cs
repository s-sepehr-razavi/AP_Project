using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Utility
{
    public static class Evaluator
    {
        public static bool checkName(this string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z]{3,32}$");
        }

        public static bool EmailPartion(this string name)
        {
            return Regex.IsMatch(name, @"^.{3,32}$");
        }

        public static bool checkEmail(this string email)
        {
            string[] s1 = email.Split('@');
            if (s1.Length != 2)
            {
                return false;
            }
            string[] s2 = s1[1].Split('.');
            if (s2.Length != 2)
            {
                return false;
            }

            if (!s1[0].EmailPartion() || !s2[0].EmailPartion())
            {
                return false;
            }

            return Regex.IsMatch(s2[1], @"^[a-zA-Z]{2,3}$");
        }

        public static bool checkPhonenumber(this string phonenumber)
        {
            return Regex.IsMatch(phonenumber, @"^09\d{9}$");
        }

        public static bool checkCustomerPassword(this string customerpassword)
        {
            return Regex.IsMatch(customerpassword, @"^(?=.*[a-z])(?=.*[A-Z]).{8,32}$");
        }

        public static bool checkCVV(this string cvv)
        {
            return Regex.IsMatch(cvv, @"^\d{3,4}$");
        }

        public static bool checkSSN(this string ssn)
        {
            return Regex.IsMatch(ssn, @"^00\d{8}$");
        }

        public static bool checkEmployeeID(this string employeeID)
        {
            return Regex.IsMatch(employeeID, @"^\d{2}9\d{2}$");
        }

        public static bool checkDate(int year, int month, int day)
        {
            DateTime dateTime = new DateTime(year, month, day);
            return dateTime >= DateTime.Now;
        }

        public static bool validateLuhnAlgorithm(this string number)
        {
            int sum = 0;
            bool isSecondDigit = false;

            // Traverse the number from right to left
            for (int i = number.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(number[i]))
                {
                    // Ignore non-digit characters
                    continue;
                }

                int digit = number[i] - '0';

                if (isSecondDigit)
                {
                    // Double every second digit
                    digit *= 2;

                    if (digit > 9)
                    {
                        // If the doubled digit is greater than 9, sum its digits
                        digit = digit / 10 + digit % 10;
                    }
                }

                sum += digit;
                isSecondDigit = !isSecondDigit;
            }

            // The number is valid if the sum is divisible by 10
            return sum % 10 == 0;
        }

    }
}
