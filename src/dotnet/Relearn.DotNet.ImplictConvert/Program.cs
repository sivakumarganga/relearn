// See https://aka.ms/new-console-template for more information
string? myVariable = "Hello World";
Console.WriteLine(myVariable);
myVariable = null;
Console.WriteLine(myVariable?.ToString());
myVariable = null;
//Console.WriteLine(myVariable!.ToString());
Console.WriteLine("End of the program");