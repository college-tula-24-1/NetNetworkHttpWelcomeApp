using System.Net.Http.Json;

HttpClient client = new();
string domenName = "https://localhost:7158";

await GetAll();
//await GetOne(5);
//await PostNew("Poppy", 22);
Console.WriteLine();

//await PutOld(2, "Bobby Smith", 28);
await DeleteOne(5);

Console.WriteLine();
await GetAll();

async Task GetAll()
{
    List<Employee>? employees = await client.GetFromJsonAsync<List<Employee>>(domenName);

    Console.WriteLine("Employees List:");
    foreach(var e in employees)
        Console.WriteLine($"Id: {e.Id} Name: {e.Name} Age: {e.Age}");
}

async Task GetOne(int id)
{
    using var response = await client.GetAsync($"{domenName}/{id}");

    if(response.StatusCode == System.Net.HttpStatusCode.OK)
    {
        Employee? employee = await response.Content.ReadFromJsonAsync<Employee>();

        Console.WriteLine("Employee found:");
        Console.WriteLine($"Id: {employee.Id} Name: {employee.Name} Age: {employee.Age}");
    }
    else
    {
        Error? error = await response.Content.ReadFromJsonAsync<Error>();
        Console.WriteLine($"Error: {error.Message}");
    }
}

async Task PostNew(string name, int age)
{
    Employee? employee = new() { Name = name, Age = age };
    var response = await client.PostAsJsonAsync(domenName, employee);

    employee = await response.Content.ReadFromJsonAsync<Employee>();
    Console.WriteLine($"Id: {employee.Id} Name: {employee.Name} Age: {employee.Age}");
}

async Task PutOld(int id, string name, int age)
{
    Employee? employee = new() { Id = id, Name = name, Age = age };

    var response = await client.PutAsJsonAsync(domenName, employee);

    if (response.StatusCode == System.Net.HttpStatusCode.OK)
    {
        employee = await response.Content.ReadFromJsonAsync<Employee>();
        Console.WriteLine($"Id: {employee.Id} Name: {employee.Name} Age: {employee.Age}");
    }
    else
    {
        Error? error = await response.Content.ReadFromJsonAsync<Error>();
        Console.WriteLine($"Error: {error.Message}");
    }
}

async Task DeleteOne(int id)
{
    var response = await client.DeleteAsync($"{domenName}/{id}");

    if(response.StatusCode == System.Net.HttpStatusCode.OK)
    {
        Employee? employee = await response.Content.ReadFromJsonAsync<Employee>();
        Console.WriteLine("Deleted Employee:");
        Console.WriteLine($"Id: {employee.Id} Name: {employee.Name} Age: {employee.Age}");
    }
    else
    {
        Error? error = await response.Content.ReadFromJsonAsync<Error>();
        Console.WriteLine($"Error: {error.Message}");
    }
}
class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
}

record Error(string Message);