namespace Lifestyle.Planning.WebApi.SelfHost
{
    using System;
    using Microsoft.Owin.Hosting;

    static class Program
    {
        static void Main()
        {
            var baseAddress = "http://localhost:8081/";

            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Lifestyle Planning application is up.");
                Console.ReadLine();
            }
        }
    }
}
