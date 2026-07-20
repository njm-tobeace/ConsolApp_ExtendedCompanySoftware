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

        // Add new employee
        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
            SaveToFile();
        }

        // Remove employee by ID
        public bool RemoveEmployee(Guid id)
        {
            var employee = employees.Find(e => e.EmployeeId == id);

            if (employee == null)
                return false;

            employees.Remove(employee);
            SaveToFile();
            return true;
        }

        // Update employee
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

        // Get all employees
        public List<Employee> GetAllEmployees()
        {
            return employees;
        }

        // Save to JSON file
        public void SaveToFile()
        {
            var json = JsonSerializer.Serialize(employees, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
        }

        // Load from JSON file
        public void LoadFromFile()
        {
            if (!File.Exists(filePath))
                return;

            var json = File.ReadAllText(filePath);

            employees = JsonSerializer.Deserialize<List<Employee>>(json)
                        ?? new List<Employee>();
        }
    }
}