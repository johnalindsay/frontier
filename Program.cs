using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Net;
using System.Net.Http;

namespace WebApplication2
{
    public class Program
    {
        private static readonly HttpClient RESTClient = new HttpClient();
        public static List<UserAccounts> UserAccountsList;

        public static async Task Main(string[] args)
        {
            UserAccountsList = await GetDataFromREST();

            CreateHostBuilder(args).Build().Run();
        }

        private static async Task<List<UserAccounts>> GetDataFromREST() 
        {
            //RESTClient.DefaultRequestHeaders.Accept.Clear();
            //RESTClient.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            //RESTClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            //var stringTask = RESTClient.GetStringAsync("https://frontiercodingtests.azurewebsites.net/api/accounts/getall");
            //var msg = await stringTask;

            var streamTask = RESTClient.GetStreamAsync("https://frontiercodingtests.azurewebsites.net/api/accounts/getall");
            var customers = await JsonSerializer.DeserializeAsync<List<UserAccounts>>(await streamTask);
            return customers;
            
        }

            public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
