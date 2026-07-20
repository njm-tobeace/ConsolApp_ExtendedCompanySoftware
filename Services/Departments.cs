
using System;
using System.Collections;

namespace ExtendedCompanySoftware.Department
{
    public class Department
    {
        public string DepartmentName { get; set; }

        public Department(string departmentName)
        {
            DepartmentName = departmentName;
        }

        public Department()
        {
            DepartmentName = "Beginners' Area";
        }
    }
}