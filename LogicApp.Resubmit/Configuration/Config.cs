using System;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace LogicApp.Resubmit.Configuration
{
    internal static class Config
    {
        public static readonly HttpClient Client = new HttpClient();
        public static readonly JsonSettings Settings = new JsonSettings();

        public static void StartupConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //Load AppSettings according to Environment

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

       
            var configuration = builder.Build();

            configuration.Bind(Settings);
         
        }
    }
}