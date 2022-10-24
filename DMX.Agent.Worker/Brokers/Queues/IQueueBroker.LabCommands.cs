// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace DMX.Agent.Worker.Brokers.Queues
{
    public partial interface IQueueBroker
    {
        void ListenToLabCommandsQueue(Func<Message, CancellationToken, Task> eventHandler);
    }
}
