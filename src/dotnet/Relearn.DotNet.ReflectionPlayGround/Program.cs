namespace Relearn.DotNet.ReflectionPlayGround
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            calculator.Sum(1, 2, 3);
            Employee employee = new Employee(1, "John", 1000);
            MyExtensionMethods.ToJson(employee);
            employee.ToJson();
            Department department = new Department();
            department.ToJson();

            //employee.GetCode();
            ////object obj = employee;
            //var myType=employee.GetType();
            Type type = typeof(Employee);

            Console.WriteLine("Properties of Employee class:{0}", type.FullName);
            foreach (var constructor in type.GetConstructors())
            {
                Console.WriteLine(constructor.Name);
            }
            foreach (var prop in type.GetProperties())
            {
                Console.WriteLine(prop.Name);
            }

            object myCustomObject = Activator.CreateInstance(type, 1, "Siva", 0M)!;
            object myObjectWithDept = Activator.CreateInstance(type, new Department())!;

            Console.WriteLine("Hello, World!");


        }
    }
    public class Calculator
    {

        public int Sum(int a, int b)
        {
            return a + b;
        }
        public int Sum(decimal a, decimal b)
        {
            return (int)(a + b);
        }
        public int Sum(int a, int b, int c)
        {
            return a + b + c;
        }
        public int Sum(int a, int b, int c, int d)
        {
            return a + b + c + d;
        }
    }
    public class Employee
    {
        public Employee()
        {

        } 
        public Employee(Department dept)
        {

        }
        public Employee(int id) : this()
        {
            this.Id = id;
        }
        public Employee(int id, string name, decimal salary) : this(id)
        {
            this.Name = name;
            this.Salary = salary;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string GetCode()
        {
            return $"{Id}:{Name}";
        }
    }
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
