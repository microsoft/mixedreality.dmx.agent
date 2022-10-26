// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Net.Http;
using DMX.Agent.Worker.Brokers.DmxApis;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Services.Foundations.LabWorkflowCommands;
using Moq;
using RESTFulSense.Exceptions;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabWorkflowCommands
{
    public partial class LabWorkflowCommandServiceTests
    {
        private readonly Mock<IDmxApiBroker> dmxApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILabWorkflowCommandService labWorkflowCommandService;

        public LabWorkflowCommandServiceTests()
        {
            this.dmxApiBrokerMock = new Mock<IDmxApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.labWorkflowCommandService = new LabWorkflowCommandService(
                dmxApiBroker: this.dmxApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData CriticalDependencyException()
        {
            string someMessage = GetRandomString();
            var someResponseMessage = new HttpResponseMessage();

            return new TheoryData<Xeption>()
            {
                new HttpResponseUrlNotFoundException(someResponseMessage, someMessage),
                new HttpResponseUnauthorizedException(someResponseMessage, someMessage),
                new HttpResponseForbiddenException(someResponseMessage, someMessage)
            };
        }

        private static LabWorkflowCommand CreateRandomLabWorkflowCommand() =>
            CreateLabWorkflowCommandFiller(GetRandomDateTimeOffset()).Create();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();


        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static Filler<LabWorkflowCommand> CreateLabWorkflowCommandFiller(DateTimeOffset dateTimeOffset)
        {
            var filler = new Filler<LabWorkflowCommand>();

            filler.Setup()
                .OnType<DateTimeOffset>()
                    .Use(dateTimeOffset);

            return filler;
        }
        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);
    }
}
