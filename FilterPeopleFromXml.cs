using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;

public class CaseStudy {

    public class Person // Helper class to store person details
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
        public DateOnly HireDate { get; set; }
        public Person(string name, int age, string department, int salary, DateOnly hireDate)
        {
            Name = name;
            Age = age;
            Department = department;
            Salary = salary;
            HireDate = hireDate;
        }
    }
    public static string FilterPeopleFromXml(string xmlData)
    {
        if (string.IsNullOrWhiteSpace(xmlData)) // Handle empty input
        {
            return "[]";
        }

        XDocument document = XDocument.Parse(xmlData);
        List<Person> people = new List<Person>();

        people = document.Descendants("Person") // Parse XML and filter people based on criteria
            .Select(p => new Person(
                p.Element("Name")?.Value,
                int.Parse(p.Element("Age")?.Value ?? "0"),
                p.Element("Department")?.Value,
                int.Parse(p.Element("Salary")?.Value ?? "0"),
                DateOnly.Parse(p.Element("HireDate")?.Value ?? DateOnly.MinValue.ToString())
            ))
            .Where(p => p.Age > 30 && p.Department == "IT" && p.Salary > 5000 && p.HireDate < new DateOnly(2019, 1, 1))
            .OrderBy(p => p.Name) // Alphabetical order by name
            .ToList();

        List<string> names = new List<string>();
        int totalSalary = 0;
        int avgSalary = 0;
        int highestSalary = 0;
        int peopleCount = 0;
        foreach (var p in people) // Update values based on filtered people
        {
            names.Add(p.Name);

            totalSalary += p.Salary; // Find total salary

            peopleCount++;

            if (p.Salary > highestSalary) // Find the highest salary
            {
                highestSalary = p.Salary;
            }
        }
        avgSalary = totalSalary / peopleCount;
        return JsonSerializer.Serialize(new   // Serialize the result as JSON
        {
            Names = names,
            TotalSalary = totalSalary,
            AverageSalary = avgSalary,
            MaxSalary = highestSalary,
            Count = peopleCount 
        }); 
    }

}
