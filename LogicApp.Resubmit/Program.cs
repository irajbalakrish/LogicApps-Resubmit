using System;
using System.Threading.Tasks;
using LogicApp.Resubmit.Azure;
using LogicApp.Resubmit.Configuration;

namespace LogicApp.Resubmit
{
    internal class Program
    {
         static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Config.StartupConfiguration();

            await ResubmitLogicApp.RetrieveTokenAsync();
        }
    }
}