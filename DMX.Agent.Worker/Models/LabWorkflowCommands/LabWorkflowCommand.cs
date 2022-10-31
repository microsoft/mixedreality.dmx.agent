// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.LabWorkflows;
using System;
using System.Text.Json.Serialization;

namespace DMX.Agent.Worker.Models.LabWorkflowCommands
{
    public class LabWorkflowCommand
    {
        public Guid Id { get; set; }
        public CommandType Type { get; set; }
        public string Arguments { get; set; }
        public Guid LabId { get; set; }
        public Guid LabWorkflowId { get; set; }
        public CommandStatus Status { get; set; }
        public string Notes { get; set; }
        public ulong CreatedBy { get; set; }
        public ulong UpdatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public string Results { get; set; }

        [JsonIgnore]
        public LabWorkflow LabWorkflow { get; set; }
    }
}