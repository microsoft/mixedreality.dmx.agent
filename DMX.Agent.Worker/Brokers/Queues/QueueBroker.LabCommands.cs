// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace DMX.Agent.Worker.Brokers.Queues
{
    public partial class QueueBroker
    {
        public IQueueClient labCommandQueue { get; set; }

        public void ListenToLabCommandsQueue(Func<Message, CancellationToken, Task> eventHandler)
        {
            MessageHandlerOptions messageHandlerOptions = GetMessageHandlerOptions();

            Func<Message, CancellationToken, Task> listenerFunction =
                CompleteEffortsQueueMessageAsync(eventHandler);

            this.labCommandQueue.RegisterMessageHandler(listenerFunction, messageHandlerOptions);
        }

        private Func<Message, CancellationToken, Task> CompleteEffortsQueueMessageAsync(
            Func<Message, CancellationToken, Task> eventHandler)
        {
            return async (message, token) =>
            {
                await eventHandler(message, token);
                await this.labCommandQueue.CompleteAsync(message.SystemProperties.LockToken);
            };
        }
    }
}
