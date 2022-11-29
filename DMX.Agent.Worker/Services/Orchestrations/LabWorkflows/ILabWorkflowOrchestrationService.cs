// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;

namespace DMX.Agent.Worker.Services.Orchestrations
{
    public interface ILabWorkflowOrchestrationService
    {
        void ListenToLabWorkflowEvents();
        ValueTask ProcessLabWorkflow(LabWorkflow labWorkflow);
    }
}
