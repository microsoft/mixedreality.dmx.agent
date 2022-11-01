// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.LabWorkflows;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowEvents
{
    public interface ILabWorkflowEventService
    {
        void ListenToLabWorkflowEvent(Func<LabWorkflow, ValueTask> labWorkflowEventHandler);
    }
}
