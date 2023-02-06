// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using LeVent.Clients;

namespace DMX.Agent.Worker.Brokers.Events
{
    public partial class EventBroker
    {
        public EventBroker()
        {
            this.LabWorkflowCommandEvents = 
                new LeVentClient<LabWorkflowCommand>();
        }
    }
}
