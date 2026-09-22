var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

int id = 0;
List<Employee> employees = new()
{
    new(){ Id = ++id, Name = "Mikky", Age = 29 },
    new(){ Id = ++id, Name = "Bobby", Age = 32 },
    new(){ Id = ++id, Name = "Sammy", Age = 24 },
};

app.MapGet("/", () => employees);


app.MapGet("/{id?}", (int? id) =>
{
    if(id is null)
        return Results.BadRequest(new { Message = "Nullable request"});
    else if(id < 1 || id > 3)
        return Results.NotFound(new { Message = $"Employee with id {id} not found" });
    else
    {
        Employee employee = employees.FirstOrDefault(e => e.Id == id)!;
        return Results.Json(employee);
    }
});

app.MapPost("/", (Employee employee) =>
{
    employee.Id = ++id;
    employees.Add(employee);
    return Results.Json(employee);
});

app.MapPut("/", (Employee employeeClient) =>
{
    var employee = employees.FirstOrDefault(e => e.Id == employeeClient.Id);
    if(employee is null)
        return Results.NotFound(new { Message = $"Employee with id {id} not found" });

    employee.Name = employeeClient.Name;
    employee.Age = employeeClient.Age;

    return Results.Json(employee);
});

app.MapDelete("/{id}", (int id) =>
{
    var employee = employees.FirstOrDefault(e => e.Id == id);
    if (employee is null)
        return Results.NotFound(new { Message = $"Employee with id {id} not found" });

    employees.Remove(employee);

    return Results.Json(employee);
});

app.Run();

class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
}
    
