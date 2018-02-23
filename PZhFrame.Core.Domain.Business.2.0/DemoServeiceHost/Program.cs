using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DemoServeiceHost
{
    public class Program
    {
        const string url = "http://localhost:50012";
        public static void Main(string[] args)
        {
            BuildWebHost(url).Run();
        }

        public static IWebHost BuildWebHost(string url) =>
            new WebHostBuilder()
            .UseUrls(url)
            .UseKestrel()
            .UseConfiguration(
                new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build())
            .UseStartup<Startup>()
            .Build();
    }
}
