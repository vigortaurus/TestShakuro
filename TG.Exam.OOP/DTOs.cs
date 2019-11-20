﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Exam.OOP
{

    public class Employee: Extend
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
   }

    public class SalesManager : Employee
    {
        public int BonusPerSale { get; set; }
        public int SalesThisMonth { get; set; }
    }

    public class CustomerServiceAgent : Employee
    {
        public int Customers { get; set; }
    }

    public class Dog: Extend
    {
        public string Name { get; set; }
        public int Age { get; set; }
   }

    public class Extend
    {
       public string ToString2()
       {
          return this.ToString();
       }
   }
}
