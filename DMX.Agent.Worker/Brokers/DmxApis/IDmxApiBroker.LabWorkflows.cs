// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;

namespace DMX.Agent.Worker.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask<LabWorkflow> PutLabWorkflowAsync(LabWorkflow labWorkflow);
    }
}
