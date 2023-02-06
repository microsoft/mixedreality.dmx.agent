// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using LeVent.Clients;

namespace DMX.Agent.Worker.Brokers.Events
{
    public partial class EventBroker
    {
        public ILeVentClient<LabWorkflowCommand> LabWorkflowCommandEvents { get; set; }

        public async ValueTask PublishLabWorkflowCommandEventAsync(
            LabWorkflowCommand labWorkflowCommand,
            string eventName = null)
        {
            await this.LabWorkflowCommandEvents.PublishEventAsync(
                labWorkflowCommand,
                eventName);
        }

        public void ListenToLabWorkflowCommandEvent(
            Func<LabWorkflowCommand, ValueTask> labWorkflowCommandEventHandler,
            string eventName = null)
        {
            this.LabWorkflowCommandEvents.RegisterEventHandler(
                labWorkflowCommandEventHandler,
                eventName);
        }
    }
}
