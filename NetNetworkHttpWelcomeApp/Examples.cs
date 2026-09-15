using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetNetworkHttpWelcomeApp
{
    public static class Examples
    {
        public async static Task HttpClientGetExampleAsync()
        {
            using (HttpClient client = new())
            {
                //var response = await httpClient.GetAsync("https://tula.top-academy.ru");
                //Console.WriteLine(response.Content.ReadAsStringAsync());

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://tula.semeynaya.ru/");

                HttpResponseMessage response = await client.SendAsync(request);

                Console.WriteLine($"Status Code: {response.StatusCode}");

                Console.WriteLine("Headers:");
                foreach (var header in response.Headers)
                {
                    Console.WriteLine($"\tHeader: {header.Key}");
                    foreach (var value in header.Value)
                        Console.WriteLine($"\t\t{value}");
                }

                Console.WriteLine("\nContent:");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
