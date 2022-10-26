// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace DMX.Agent.Worker.Brokers.Queues
{
    public partial class QueueBroker
    {
        public IQueueClient labWorkflowQueue { get; set; }

        public void ListenToLabWorkflowsQueue(Func<Message, CancellationToken, Task> eventHandler)
        {
            MessageHandlerOptions messageHandlerOptions = GetMessageHandlerOptions();

            Func<Message, CancellationToken, Task> listenerFunction =
                CompleteLabWorkflowsQueueMessageAsync(eventHandler);

            this.labWorkflowQueue.RegisterMessageHandler(listenerFunction, messageHandlerOptions);
        }

        private Func<Message, CancellationToken, Task> CompleteLabWorkflowsQueueMessageAsync(
            Func<Message, CancellationToken, Task> eventHandler)
        {
            return async (message, token) =>
            {
                await eventHandler(message, token);
                await this.labWorkflowQueue.CompleteAsync(message.SystemProperties.LockToken);
            };
        }
    }
}
