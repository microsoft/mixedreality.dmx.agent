﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;

namespace DMX.Agent.Worker.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask<LabWorkflowCommand> PostLabWorkflowCommandAsync(LabWorkflowCommand labWorkflowCommand);
        ValueTask<LabWorkflowCommand> GetLabWorkflowCommandByIdAsync(Guid labWorkflowCommandId);
        ValueTask<LabWorkflowCommand> PutLabWorkflowCommandAsync(LabWorkflowCommand labWorkflowCommand);
    }
}
