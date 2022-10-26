// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Text;
using System.Threading.Tasks;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Brokers.Queues;
using DMX.Agent.Worker.Models.Foundations.LabCommands;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace DMX.Agent.Worker.Services.Foundations.LabCommandEvents
{
    public partial class LabCommandEventService : ILabCommandEventService
    {
        private readonly IQueueBroker queueBroker;
        private readonly ILoggingBroker loggingBroker;

        public LabCommandEventService(IQueueBroker queueBroker, ILoggingBroker loggingBroker)
        {
            this.queueBroker = queueBroker;
            this.loggingBroker = loggingBroker;
        }

        public void ListenToLabCommandEvent(Func<LabCommand, ValueTask> labCommandEventHandler) =>
        TryCatch(() =>
        {
            ValidateLabCommandEventHandler(labCommandEventHandler);

            this.queueBroker.ListenToLabCommandsQueue(async (message, token) =>
            {
                LabCommand incomingLabCommand = MapToLabCommand(message);
                await labCommandEventHandler(incomingLabCommand);
            });
        });

        private static LabCommand MapToLabCommand(Message message)
        {
            var stringifiedLabCommand =
                    Encoding.UTF8.GetString(message.Body);

            var labCommandEvent =
                JsonConvert.DeserializeObject<LabCommand>(
                    stringifiedLabCommand);

            return labCommandEvent;
        }
    }
}
