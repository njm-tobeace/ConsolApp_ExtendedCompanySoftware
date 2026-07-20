
using System;
using System.Collections;

namespace ExtendedCompanySoftware.Salary
{
    public class Salary
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public Salary(decimal amount, string currency = "EUR")
        {
            Currency = currency;
            Amount = amount;
        }

        public Salary()
        {
            Amount = 0;
            Currency = "EUR";
        }
    }
}