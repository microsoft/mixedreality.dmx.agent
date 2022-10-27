﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Text.Json.Serialization;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands
{
    public class LabWorkflowCommand
    {
        public Guid Id { get; set; }
        public Guid WorkflowId { get; set; }
        public CommandType Type { get; set; }
        public string Arguments { get; set; }
        public Guid LabId { get; set; }
        public CommandStatus Status { get; set; }
        public string Notes { get; set; }
        public ulong CreatedBy { get; set; }
        public ulong UpdatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public string Results { get; set; }

        [JsonIgnore]
        public LabWorkflow Workflow { get; set; }
    }
}
