// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Linq;
using DMX.Agent.Worker.Brokers.Queues;

namespace DMX.Agent.Worker.Services.Foundations.LabCommandEvents
{
    public partial class LabCommandEventService : ILabCommandEventService
    {
        private readonly IQueueBroker queueBroker;
        private readonly ILoggingBroker loggingBroker;

        
        public void RecieveLabCommandEvent()
        {
            throw new System.NotImplementedException();
        }
    }
}
