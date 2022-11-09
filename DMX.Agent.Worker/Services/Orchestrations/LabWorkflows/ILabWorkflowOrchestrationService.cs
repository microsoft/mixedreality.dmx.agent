// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using System.Threading.Tasks;

namespace DMX.Agent.Worker.Services.Orchestrations
{
    public interface ILabWorkflowOrchestrationService
    {
        void ListenToLabWorkflowEvents();
        ValueTask ProcessLabWorkflow(LabWorkflow labWorkflow);
    }
}
