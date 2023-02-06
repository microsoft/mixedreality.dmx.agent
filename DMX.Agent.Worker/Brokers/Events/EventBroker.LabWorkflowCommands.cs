// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using System.Threading.Tasks;
using System;
using LeVent.Clients;

namespace DMX.Agent.Worker.Brokers.Events
{
    public partial class EventBroker
    {
        public ILeVentClient<LabWorkflowCommand> LabWorkflowCommandEvents { get; set; }

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
