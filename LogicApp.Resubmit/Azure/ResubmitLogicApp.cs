using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LogicApp.Resubmit.Configuration;
using LogicApp.Resubmit.Dto;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace LogicApp.Resubmit.Azure
{
    public class ResubmitLogicApp
    {
        private static readonly List<string> Workflowruns = new List<string>();

        public static async Task RetrieveTokenAsync()
        {
            try
            {
                var authString = Config.Settings.Aad.AadInstance + Config.Settings.Aad.TenantId;
                var resourceUrl = Config.Settings.App.Resource;
                var authenticationContext = new AuthenticationContext(authString, false);
                var clientCred = new ClientCredential(Config.Settings.App.ClientId,
                    Config.Settings.App.ClientSecret);
                var authenticationResult = await authenticationContext.AcquireTokenAsync(resourceUrl, clientCred);
                var token = authenticationResult.AccessToken;
                Console.WriteLine("GetAccessToken Succeded : " + token);
                await GetWorkflowHistoryRuns(token);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task GetWorkflowHistoryRuns(string token)
        {
            try
            {
                Config.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var endpoint = string.Format(Config.Settings.Endpoint.GetWorkflowRuns,
                    Config.Settings.Azureuri.Subscriptionid, Config.Settings.Azureuri.Resourcegroupname,
                    Config.Settings.Azureuri.Logicappname,
                    Config.Settings.Azureuri.Filterstatus, Config.Settings.Azureuri.Starttime);
                await GetNextLink(endpoint);
                await ResubmitFailedLogicApps();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private static async Task ResubmitFailedLogicApps()
        {
            try
            {
                Parallel.ForEach(Workflowruns.Take(10).ToList(), x =>
                {
                    var endpoint = string.Format(Config.Settings.Endpoint.Resubmitworkflowtriggers,
                        Config.Settings.Azureuri.Subscriptionid, Config.Settings.Azureuri.Resourcegroupname,
                        Config.Settings.Azureuri.Logicappname,
                        Config.Settings.Azureuri.Trigger, x);

                    var result = Config.Client.PostAsync(endpoint, null).ConfigureAwait(false).GetAwaiter().GetResult();
                    Console.WriteLine($"Post Status for {x} : {result.StatusCode}");
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private static async Task GetNextLink(string nextlink)
        {
            while (true)
            {
                var result = Config.Client.GetAsync(nextlink).ConfigureAwait(false).GetAwaiter().GetResult();
                var content = await result.Content.ReadAsStringAsync();
                var workflowhistory = JsonConvert.DeserializeObject<WorkFlowRuns>(content);
                foreach (var item in workflowhistory.Value)
                {
                    Workflowruns.Add(item.Name);
                    Console.WriteLine($"Failed Run ID : {item.Name}");
                }

                if (!string.IsNullOrEmpty(workflowhistory.NextLink))
                {
                    nextlink = workflowhistory.NextLink;
                    continue;
                }

                break;
            }
        }
    }
}