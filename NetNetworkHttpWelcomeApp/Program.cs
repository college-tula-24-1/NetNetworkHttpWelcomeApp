using System.Net.Http.Json;

using (HttpClient client = new())
{
    //object? data = await client.GetFromJsonAsync("https://localhost:7068/", typeof(Person));
    //if(data is Person person)
    //    Console.WriteLine($"Person name: {person.Name}, age: {person.Age}");

    Console.Write("Input id person: ");
    int? id = Int32.Parse(Console.ReadLine());

    using var response = await client.GetAsync($"https://localhost:7158/{id}");

    if(response.StatusCode == System.Net.HttpStatusCode.BadRequest
        || response.StatusCode == System.Net.HttpStatusCode.NotFound)
    {
        Error? error = await response.Content.ReadFromJsonAsync<Error>();
        Console.WriteLine($"Status Code: {response.StatusCode}, Message: {error?.Message}");
    }
    else
    {
        Person? person = await response.Content.ReadFromJsonAsync<Person>();
        Console.WriteLine(person);
    }

}

public record class Person(int Id, string Name, int Age);

public record class Error(string Message);