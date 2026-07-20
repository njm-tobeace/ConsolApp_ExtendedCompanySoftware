
using System;
using System.Collections;
using ExtendedCompanySoftware.Salary;
using ExtendedCompanySoftware.Department;

namespace ExtendedCompanySoftware.Employee
{
    public class Employee
    {
        public Guid EmployeeId { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Department Department { get; set; }
        public Salary Salary { get; set; }

        public Employee(string firstName, string lastName)
        {
            EmployeeId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;

            Department = new Department();
            Salary = new Salary();
        }
    }
}