// See https://aka.ms/new-console-template for more information
using System.Linq;

Console.WriteLine("Hello, World!");

int a = 5, b = 10;
int sum = a * b / 5;
List<Employee> employees = new()
{
    new Employee { Id = 1, Name = "John", Salary = 1000 },
    new Employee { Id = 2, Name = "Jane", Salary = 1500 },
    new Employee { Id = 3, Name = "Siva", Salary = 2000 }
};

decimal minimumSalary = 10000;
decimal totalSalary = employees.Aggregate(minimumSalary, (total, emp)  => total + emp.Salary);
Console.WriteLine($"Total Salary: {totalSalary}");



public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Salary { get; set; }
}