
using System;
using System.Collections;
using ExtendedCompanySoftware.Employee;

namespace ExtendedCompanySoftware.Salary
{
    public class Salary
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        // Set Salary
        public Salary(decimal amount, string currency)
        {
            Currency = currency;
            Amount = amount;
        }

        // Default Salary
        public Salary()
        {
            Amount = 0;
            Currency = "EUR";
        }
    }
}