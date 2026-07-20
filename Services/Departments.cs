
using System;
using System.Collections;

namespace ExtendedCompanySoftware.Department
{
    public class Department
    {
        public string DepartmentName { get; set; }

        // Set Department
        public Department(string departmentName)
        {
            DepartmentName = departmentName;
        }

        // Default Department
        public Department()
        {
            DepartmentName = "Beginners' Area";
        }
    }
}