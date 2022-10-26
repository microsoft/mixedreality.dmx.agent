// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace DMX.Agent.Worker.Brokers.Queues
{
    public partial class QueueBroker : IQueueBroker
    {
        private readonly string serviceBusConnection;

        public QueueBroker(string serviceBusConnection)
        {
            this.serviceBusConnection = serviceBusConnection;
            InitializeQueue();
        }

        private void InitializeQueue()
        {
            this.labCommandQueue = GetQueueClient(nameof(this.labCommandQueue));
            this.labWorkflowQueue = GetQueueClient(nameof(this.labWorkflowQueue));
        }

        private MessageHandlerOptions GetMessageHandlerOptions()
        {
            return new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            };
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            ExceptionReceivedContext context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            return Task.CompletedTask;
        }

        private IQueueClient GetQueueClient(string queueName) =>
            new QueueClient(this.serviceBusConnection, queueName);
    }
}
