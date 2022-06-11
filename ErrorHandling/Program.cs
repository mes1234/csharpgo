using System.Net.Http;

namespace ErrorHandling;

public static class Program
{
    public static void Main(string[] args)
    {

        var variable = "1";

        try
        {
            var converted = int.Parse(variable);

            Console.WriteLine("Hurray I can add 1 to {0} and get {1}\n", converted, converted + 1);
        }
        catch (System.FormatException ex)
        {
            Console.WriteLine("cannot conver error: {0}\n", ex.Message);

            Environment.Exit(1);
        }
        try
        {
            var client = new HttpClient();

            var response = client.GetAsync("http://worldtimeapi.org/api/ip").Result;

            response.EnsureSuccessStatusCode();

            var body = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine("Current time here and now is {0}\n", body);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("cannot get data error {0}", ex.Message);

            Environment.Exit(1);
        }

    }
}
