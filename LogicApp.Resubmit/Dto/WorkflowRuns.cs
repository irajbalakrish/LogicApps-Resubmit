using System;
using System.Collections.Generic;

namespace LogicApp.Resubmit.Dto
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class Correlation
    {
        public string ClientTrackingId { get; set; }
    }

    public class Workflow
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class ContentHash
    {
        public string Algorithm { get; set; }
        public string Value { get; set; }
    }

    public class InputsLink
    {
        public string Uri { get; set; }
        public string ContentVersion { get; set; }
        public int ContentSize { get; set; }
        public ContentHash ContentHash { get; set; }
    }

    public class ContentHash2
    {
        public string Algorithm { get; set; }
        public string Value { get; set; }
    }

    public class OutputsLink
    {
        public string Uri { get; set; }
        public string ContentVersion { get; set; }
        public int ContentSize { get; set; }
        public ContentHash2 ContentHash { get; set; }
    }

    public class Correlation2
    {
        public string ClientTrackingId { get; set; }
    }

    public class Trigger
    {
        public string Name { get; set; }
        public InputsLink InputsLink { get; set; }
        public OutputsLink OutputsLink { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Correlation2 Correlation { get; set; }
        public string Status { get; set; }
    }

    public class Outputs
    {
    }

    public class Properties
    {
        public DateTime WaitEndTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public Error Error { get; set; }
        public Correlation Correlation { get; set; }
        public Workflow Workflow { get; set; }
        public Trigger Trigger { get; set; }
        public Outputs Outputs { get; set; }
    }

    public class Value
    {
        public Properties Properties { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class WorkFlowRuns
    {
        public List<Value> Value { get; set; }
        public string NextLink { get; set; }
    }
}