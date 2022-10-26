// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Brokers.Queues;
using DMX.Agent.Worker.Models.LabWorkflows;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowEvents
{
    public class LabWorkflowEventService : ILabWorkflowEventService
    {
        private readonly IQueueBroker queueBroker;
        private readonly ILoggingBroker loggingBroker;

        public LabWorkflowEventService(
            IQueueBroker queueBroker,
            ILoggingBroker loggingBroker)
        {
            this.queueBroker = queueBroker;
            this.loggingBroker = loggingBroker;
        }

        public void ListenToLabWorkflowEvent(Func<LabWorkflow, ValueTask> labWorkflowEventHandler)
        {
            throw new NotImplementedException();
        }
    }
}
