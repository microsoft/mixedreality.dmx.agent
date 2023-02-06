// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;

namespace DMX.Agent.Worker.Brokers.Events
{
    public partial interface IEventBroker
    {
        ValueTask PublishLabWorkflowCommandEventAsync(
            LabWorkflowCommand labWorkflowCommand,
            string eventName = null);

        void ListenToLabWorkflowCommandEvent(
            Func<LabWorkflowCommand, ValueTask> labWorkflowCommandEventHandler,
            string eventName = null);
    }
}
