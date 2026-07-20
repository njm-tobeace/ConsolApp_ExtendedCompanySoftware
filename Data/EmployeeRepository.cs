using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ExtendedCompanySoftware.Employee;

namespace ExtendedCompanySoftware.Repository
{
    public class EmployeeRepository
    {
        private readonly string filePath = "employees.json";
        private List<Employee> employees = new List<Employee>();

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
            SaveToFile();
        }

        public bool RemoveEmployee(Guid id)
        {
            var employee = employees.Find(e => e.EmployeeId == id);
            if (employee == null)
                return false;

            employees.Remove(employee);
            SaveToFile();
            return true;
        }

        public bool UpdateEmployee(Employee updatedEmployee)
        {
            var existing = employees.Find(e => e.EmployeeId == updatedEmployee.EmployeeId);
            if (existing == null)
                return false;

            existing.FirstName = updatedEmployee.FirstName;
            existing.LastName = updatedEmployee.LastName;
            existing.Department = updatedEmployee.Department;
            existing.Salary = updatedEmployee.Salary;

            SaveToFile();
            return true;
        }

        public List<Employee> GetAllEmployees()
        {
            return employees;
        }

        public void SaveToFile()
        {
            var json = JsonSerializer.Serialize(employees, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
        }

        public void LoadFromFile()
        {
            if (!File.Exists(filePath))
                return;

            try
            {
                var json = File.ReadAllText(filePath);
                employees = JsonSerializer.Deserialize<List<Employee>>(json)
                            ?? new List<Employee>();
            }
            catch
            {
                employees = new List<Employee>();
            }
        }
    }
}