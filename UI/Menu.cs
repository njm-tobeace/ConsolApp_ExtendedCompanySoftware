
using System;
using System.Collections.Generic;
using System.Globalization;
using ExtendedCompanySoftware.Repository;
using ExtendedCompanySoftware.Employee;
using ExtendedCompanySoftware.Department;
using ExtendedCompanySoftware.Salary;

namespace ExtendedCompanySoftware.Menu
{
    public class Menu
    {
        private readonly EmployeeRepository repository = new EmployeeRepository();
        private List<Option> options;

        public void Start()
        {
            repository.LoadFromFile();

            options = new List<Option>
            {
                new Option("Add new employee", AddEmployee),
                new Option("List all employees", ShowEmployees),
                new Option("Search employee", SearchEmployee),
                new Option("Remove employee", DeleteEmployee),
                new Option("Edit employee", EditEmployee),
                new Option("Save data", () => repository.SaveToFile()),
                new Option("Exit", () => Environment.Exit(0))
            };

            int index = 0;
            DrawMenu(index);

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (index < options.Count - 1) index++;
                        break;

                    case ConsoleKey.UpArrow:
                        if (index > 0) index--;
                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        options[index].Selected.Invoke();
                        break;
                }

                DrawMenu(index);

            } while (key.Key != ConsoleKey.X);
        }

        private void DrawMenu(int selectedIndex)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Employee Management ===\n");
            Console.ResetColor();

            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }

                Console.WriteLine(options[i].Name);
                Console.ResetColor();
            }

            Console.WriteLine("\nPress X to exit the menu.");
        }

        private void AddEmployee()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Add Employee ===");
            Console.ResetColor();

            string firstName = AskString("First name");
            string lastName = AskString("Last name");

            Employee employee = new Employee(firstName, lastName);

            employee.Department.DepartmentName = AskString("Department");
            employee.Salary.Amount = AskDecimal("Salary (EUR)");

            repository.AddEmployee(employee);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEmployee added successfully.");
            Console.ResetColor();
            Pause();
        }

        private void ShowEmployees()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Employee List ===\n");
            Console.ResetColor();

            var employees = repository.GetAllEmployees();

            if (employees.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No employees found.");
                Console.ResetColor();
                Pause();
                return;
            }

            foreach (var e in employees)
            {
                Console.WriteLine($"ID: {e.EmployeeId}");
                Console.WriteLine($"Name: {e.FirstName} {e.LastName}");
                Console.WriteLine($"Department: {e.Department.DepartmentName}");
                Console.WriteLine($"Salary: {e.Salary.Amount.ToString("C", new CultureInfo("de-DE"))}");
                Console.WriteLine("-----------------------------");
            }

            Pause();
        }

        private void SearchEmployee()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Search Employee ===\n");
            Console.ResetColor();

            string query = AskString("Search term (name or department)");

            var employees = repository.GetAllEmployees();
            var results = employees.FindAll(e =>
                e.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                e.LastName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                e.Department.DepartmentName.Contains(query, StringComparison.OrdinalIgnoreCase)
            );

            if (results.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No matching employees found.");
                Console.ResetColor();
                Pause();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{results.Count} employee(s) found:\n");
            Console.ResetColor();

            foreach (var e in results)
            {
                Console.WriteLine($"ID: {e.EmployeeId}");
                Console.WriteLine($"Name: {e.FirstName} {e.LastName}");
                Console.WriteLine($"Department: {e.Department.DepartmentName}");
                Console.WriteLine($"Salary: {e.Salary.Amount.ToString("C", new CultureInfo("de-DE"))}");
                Console.WriteLine("-----------------------------");
            }

            Pause();
        }

        private void DeleteEmployee()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Remove Employee ===");
            Console.ResetColor();

            Guid id = AskGuid("Enter employee ID");

            if (repository.RemoveEmployee(id))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Employee removed.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee not found.");
            }

            Console.ResetColor();
            Pause();
        }

        private void EditEmployee()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Edit Employee ===");
            Console.ResetColor();

            Guid id = AskGuid("Enter employee ID");

            var employees = repository.GetAllEmployees();
            var employee = employees.Find(e => e.EmployeeId == id);

            if (employee == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee not found.");
                Console.ResetColor();
                Pause();
                return;
            }

            Console.WriteLine("\nPress Enter to keep the current value.\n");

            string firstName = AskOptionalString($"New first name ({employee.FirstName})");
            if (firstName != null) employee.FirstName = firstName;

            string lastName = AskOptionalString($"New last name ({employee.LastName})");
            if (lastName != null) employee.LastName = lastName;

            string dept = AskOptionalString($"New department ({employee.Department.DepartmentName})");
            if (dept != null) employee.Department.DepartmentName = dept;

            decimal? salary = AskOptionalDecimal($"New salary ({employee.Salary.Amount})");
            if (salary != null) employee.Salary.Amount = salary.Value;

            repository.UpdateEmployee(employee);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEmployee updated.");
            Console.ResetColor();
            Pause();
        }

        private string AskString(string label)
        {
            string input;
            do
            {
                Console.Write($"{label}: ");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        private string AskOptionalString(string label)
        {
            Console.Write($"{label}: ");
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? null : input;
        }

        private decimal AskDecimal(string label)
        {
            decimal value;
            while (true)
            {
                Console.Write($"{label}: ");
                if (decimal.TryParse(Console.ReadLine(), out value))
                    return value;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number. Try again.");
                Console.ResetColor();
            }
        }

        private decimal? AskOptionalDecimal(string label)
        {
            Console.Write($"{label}: ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return null;

            if (decimal.TryParse(input, out decimal value))
                return value;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid number. Value unchanged.");
            Console.ResetColor();
            return null;
        }

        private Guid AskGuid(string label)
        {
            Guid id;
            while (true)
            {
                Console.Write($"{label}: ");
                if (Guid.TryParse(Console.ReadLine(), out id))
                    return id;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID format. Try again.");
                Console.ResetColor();
            }
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    public class Option
    {
        public string Name { get; }
        public Action Selected { get; }

        public Option(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }
    }
}