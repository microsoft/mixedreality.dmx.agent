// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Text;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Brokers.Queues;
using DMX.Agent.Worker.Models.LabCommands;
using DMX.Agent.Worker.Services.Foundations.LabCommandEvents;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Azure.ServiceBus;
using Moq;
using Newtonsoft.Json;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;
using AzureMessagingCommunicationException = Microsoft.ServiceBus.Messaging.MessagingCommunicationException;


namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabCommandEvents
{
    public partial class LabCommandEventServiceTests
    {
        private readonly Mock<IQueueBroker> queueBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILabCommandEventService labCommandEventService;
        private readonly ICompareLogic compareLogic;

        public LabCommandEventServiceTests()
        {
            this.queueBrokerMock = new Mock<IQueueBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.compareLogic = new CompareLogic();

            this.labCommandEventService = new LabCommandEventService(
                queueBroker: this.queueBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
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

        private static Message CreateLabCommandMessage(LabCommand labCommand)
        {
            string serializedLabCommand = JsonConvert.SerializeObject(labCommand);
            byte[] labCommandBody = Encoding.UTF8.GetBytes(serializedLabCommand);

            return new Message
            {
                Body = labCommandBody
            };
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);
            

        private static LabCommand CreateRandomLabCommand() =>
            CreateLabCommandFiller(GetRandomDateTimeOffset()).Create();

        private static LabCommand CreateRandomLabCommand(DateTimeOffset date) =>
            CreateLabCommandFiller(date).Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static Filler<LabCommand> CreateLabCommandFiller(DateTimeOffset dateTimeOffset)
        {
            var filler = new Filler<LabCommand>();

            filler.Setup()
                .OnType<DateTimeOffset>()
                    .Use(dateTimeOffset);

            return filler;
        }

        private Expression<Func<LabCommand, bool>> SameLabCommandAs(LabCommand expectedLabCommand)
        {
            return actualLabCommand =>
                this.compareLogic.Compare(expectedLabCommand, actualLabCommand).AreEqual;
        }
    }
}
