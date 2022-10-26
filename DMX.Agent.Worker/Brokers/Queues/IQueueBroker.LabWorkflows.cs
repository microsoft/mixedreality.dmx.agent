// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace DMX.Agent.Worker.Brokers.Queues
{
    public partial interface IQueueBroker
    {
        void ListenToLabWorkflowsQueue(Func<Message, CancellationToken, Task> eventHandler);
    }
}
