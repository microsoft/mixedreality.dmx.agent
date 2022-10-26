// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Brokers.Queues;
using DMX.Agent.Worker.Models.LabWorkflows;
using DMX.Agent.Worker.Services.Foundations.LabWorkflowEvents;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Azure.ServiceBus;
using Moq;
using Tynamix.ObjectFiller;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabWorkflowEvents
{
    public partial class LabWorkflowEventServiceTests
    {
        private Mock<IQueueBroker> queueBrokerMock;
        private Mock<ILoggingBroker> loggingBrokerMock;
        private CompareLogic compareLogic;
        private ILabWorkflowEventService labWorkflowEventService;

        public LabWorkflowEventServiceTests()
        {
            this.queueBrokerMock = new Mock<IQueueBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.compareLogic = new CompareLogic();
            this.labWorkflowEventService = new LabWorkflowEventService(
                this.queueBrokerMock.Object,
                this.loggingBrokerMock.Object);
        }

        private Expression<Func<LabWorkflow, bool>> SameLabWorkflowAs(LabWorkflow expectedLabWorkflow) =>
            actualLabWorkflow => this.compareLogic.Compare(expectedLabWorkflow, actualLabWorkflow).AreEqual;

        private static Message CreateLabWorkflowMessage(LabWorkflow labWorkflow)
        {
            string serializedLabWorkflow = JsonSerializer.Serialize(labWorkflow);

            byte[] labWorkflowBody = Encoding.UTF8.GetBytes(serializedLabWorkflow);

            return new Message()
            {
                Body = labWorkflowBody
            };
        }

        private static Filler<LabWorkflow> CreateLabWorkflowFiller()
        {
            var filler = new Filler<LabWorkflow>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTime.UtcNow);

            return filler;
        }

        private static LabWorkflow CreateRandomLabWorkflow() =>
            CreateLabWorkflowFiller().Create();
    }
}
