// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Text;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Brokers.Queues;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using DMX.Agent.Worker.Services.Foundations.LabWorkflowEvents;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Azure.ServiceBus;
using Moq;
using Newtonsoft.Json;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;
using AzureMessagingCommunicationException = Microsoft.ServiceBus.Messaging.MessagingCommunicationException;

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

        public static TheoryData MessageQueueExceptions()
        {
            string message = GetRandomString();

            return new TheoryData<Exception>
            {
                new MessagingEntityNotFoundException(message),
                new MessagingEntityDisabledException(message),
                new UnauthorizedAccessException()
            };
        }

        public static TheoryData MessageQueueDependencyExceptions()
        {
            string message = GetRandomString();

            return new TheoryData<Exception>
            {
                new InvalidOperationException(),
                new AzureMessagingCommunicationException(communicationPath: message),
                new ServerBusyException(message),
            };
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private Expression<Func<LabWorkflow, bool>> SameLabWorkflowAs(LabWorkflow expectedLabWorkflow)
        {
            return actualLabWorkflow =>
                this.compareLogic.Compare(expectedLabWorkflow, actualLabWorkflow).AreEqual;
        }

        private Expression<Func<Exception, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);


        private static Message CreateLabWorkflowMessage(LabWorkflow labWorkflow)
        {
            string serializedLabWorkflow = JsonConvert.SerializeObject(labWorkflow);

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
