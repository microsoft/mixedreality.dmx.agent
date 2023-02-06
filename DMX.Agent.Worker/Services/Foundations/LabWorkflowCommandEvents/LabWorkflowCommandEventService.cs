// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Brokers.Events;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowCommandEvents
{
    public partial class LabWorkflowCommandEventService : ILabWorkflowCommandEventService
    {
        private readonly IEventBroker eventBroker;
        private readonly ILoggingBroker loggingBroker;

        public LabWorkflowCommandEventService(
            IEventBroker eventBroker,
            ILoggingBroker loggingBroker)
        {
            this.eventBroker = eventBroker;
            this.loggingBroker = loggingBroker;
        }

        public void RegisterLabWorkflowCommandEventHandler(
            Func<LabWorkflowCommand, ValueTask> labWorkflowCommandEventHandler,
            string eventName = null) =>
            this.eventBroker.ListenToLabWorkflowCommandEvent(
                labWorkflowCommandEventHandler,
                eventName);
    }
}
