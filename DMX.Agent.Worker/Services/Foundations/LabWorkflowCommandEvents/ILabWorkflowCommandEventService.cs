// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowCommandEvents
{
    public interface ILabWorkflowCommandEventService
    {
        void RegisterLabWorkflowCommandEventHandler(
            Func<LabWorkflowCommand, ValueTask> labWorkflowCommandEventHandler,
            string eventName = null);
    }
}
