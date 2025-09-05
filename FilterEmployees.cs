using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class CaseStudy {
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        if (employees == null) // Handle null input
        {
            return "[]";
        }

        var filteredEmployees = new List<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)>();

        foreach (var p in employees) // Filter employees based on criteria
        {
            if (p.Age >= 25 && p.Age <= 40 &&
            (p.Department == "IT" || p.Department == "Finance") &&
            p.Salary >= 5000m && p.Salary <= 9000m &&
            p.HireDate > new DateTime(2017, 1, 1))
            {
                filteredEmployees.Add(p);
            }
        }
        filteredEmployees = filteredEmployees.OrderByDescending(p => p.Name.Length).ThenBy(p => p.Name).ToList(); // Sort by name length and then alphabetically

        List<string> names = new List<string>();
        decimal totalSalary = 0;
        decimal avgSalary = 0;
        decimal lowestSalary = decimal.MaxValue;
        decimal highestSalary = 0;
        int peopleCount = 0;
        foreach (var p in filteredEmployees) // Update values based on filtered people
        {
            names.Add(p.Name);

            totalSalary += p.Salary; // Find total salary

            peopleCount++;

            if (p.Salary > highestSalary) // Find the highest salary
            {
                highestSalary = p.Salary;
            }
            if (p.Salary < lowestSalary) // Find the lowest salary
            {
                lowestSalary = p.Salary;
            }
        }
        
        if (peopleCount == 0) // Handle case with no matching employees
        {
            lowestSalary = 0;
        }
        else
        {
            avgSalary = totalSalary / peopleCount; // Avoid division by zero
        }
        
        return JsonSerializer.Serialize(new   // Serialize the result as JSON
        {
            Names = names,
            TotalSalary = totalSalary,
            AverageSalary = avgSalary,
            MinSalary = lowestSalary,
            MaxSalary = highestSalary,
            Count = peopleCount
        }); 
    }

}
