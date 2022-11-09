// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using System;
using System.Threading.Tasks;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowEvents
{
    public interface ILabWorkflowEventService
    {
        void ListenToLabWorkflowEvent(Func<LabWorkflow, ValueTask> labWorkflowEventHandler);
    }
}
