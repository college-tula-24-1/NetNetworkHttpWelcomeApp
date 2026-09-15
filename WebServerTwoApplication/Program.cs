var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Employee> employees = new()
{
    new(1, "Mikky", 29),
    new(2, "Bobby", 32),
    new(3, "Sammy", 24),
};



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

app.Run();

record class Employee(int Id, string Name, int Age);
