namespace LogicApp.Resubmit.Configuration
{
    public class Aad
    {
        public string AadInstance { get; set; }
        public string TenantId { get; set; }
    }

    public class App
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Resource { get; set; }
      
    }

    public class Endpoint
    {
        public string GetWorkflowRuns { get; set; }
        public string Resubmitworkflowtriggers { get; set; }
    }

    public class Azure
    {
        public string Subscriptionid { get; set; }
        public string Resourcegroupname { get; set; }
        public string Logicappname { get; set; }
        public string Filterstatus { get; set; }
        public string Starttime { get; set; }
        public string Trigger { get; set; }
    }

    public class JsonSettings
    {
        public Aad Aad { get; set; }
        public App App { get; set; }
        public Endpoint Endpoint { get; set; }
        public Azure Azureuri { get; set; }
    }
}