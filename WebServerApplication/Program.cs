var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new Employee("Bobby", 25));

app.Run();



public record class Employee(string Name, int Age);
