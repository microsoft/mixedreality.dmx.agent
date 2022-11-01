// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Text;
using System.Threading.Tasks;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Brokers.Queues;
using DMX.Agent.Worker.Models.LabWorkflows;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowEvents
{
    public partial class LabWorkflowEventService : ILabWorkflowEventService
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

        public void ListenToLabWorkflowEvent(Func<LabWorkflow, ValueTask> labWorkflowEventHandler) =>
        TryCatch(() =>
        {
            ValidateIfEventHandlerIsNull(labWorkflowEventHandler);

            this.queueBroker.ListenToLabWorkflowsQueue(async (message, token) =>
            {
                LabWorkflow incomingLabWorkflow = MapToLabWorkflow(message);
                await labWorkflowEventHandler(incomingLabWorkflow);
            });
        });

        private static LabWorkflow MapToLabWorkflow(Message message)
        {
            var stringifiedLabWorkflow =
                Encoding.UTF8.GetString(message.Body);

            var labWorkflowEvent =
                JsonConvert.DeserializeObject<LabWorkflow>(
                    stringifiedLabWorkflow);

            return labWorkflowEvent;
        }
    }
}
