/*
Author:     Nico-Julian M.
Date:       09.07.2026
Updated;    20.07.2026
*/

using System;
using System.Collections;
using ExtendedCompanySoftware.Menu;
using ExtendedCompanySoftware.Salary;
using ExtendedCompanySoftware.Employee;
using ExtendedCompanySoftware.Department;
using ExtendedCompanySoftware.Repository;

namespace ExtendedCompanySoftware
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Extended Company Software";

            Console.WriteLine("====================================");
            Console.WriteLine("     Extended Company Software");
            Console.WriteLine("====================================");
            Console.WriteLine("Loading...");
            Thread.Sleep(800);

            Menu menu = new Menu();
            menu.Start();
        }
    }
}