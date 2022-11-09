// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using DMX.Agent.Worker.Brokers.DateTimes;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Models.Foundations.Commands.Exceptions;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using DMX.Agent.Worker.Services.Foundations.Commands;
using DMX.Agent.Worker.Services.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Services.Foundations.LabWorkflowEvents;
using DMX.Agent.Worker.Services.Orchestrations;
using DMX.Agent.Worker.Services.Orchestrations.LabWorkflows;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Orchestrations.LabWorkflows
{
    public partial class LabWorkflowOrchestrationServiceTests
    {
        private readonly Mock<ILabWorkflowEventService> labWorkflowEventServiceMock;
        private readonly Mock<ILabWorkflowCommandService> labWorkflowCommandServiceMock;
        private readonly Mock<ICommandService> commandServiceMock;
        private readonly Mock<IDateTimeBroker> dateTimeBroker;
        private readonly Mock<ILoggingBroker> loggingBroker;
        private readonly ILabWorkflowOrchestrationService labWorkflowOrchestrationService;
        private readonly CompareLogic compareLogic;

        public LabWorkflowOrchestrationServiceTests()
        {
            this.labWorkflowEventServiceMock = new Mock<ILabWorkflowEventService>(MockBehavior.Strict);
            this.labWorkflowCommandServiceMock = new Mock<ILabWorkflowCommandService>(MockBehavior.Strict);
            this.commandServiceMock = new Mock<ICommandService>(MockBehavior.Strict);
            this.dateTimeBroker = new Mock<IDateTimeBroker>(MockBehavior.Strict);
            this.loggingBroker = new Mock<ILoggingBroker>();
            this.compareLogic = new CompareLogic();

            this.labWorkflowOrchestrationService = new LabWorkflowOrchestrationService(
                this.labWorkflowEventServiceMock.Object,
                this.labWorkflowCommandServiceMock.Object,
                this.commandServiceMock.Object,
                this.dateTimeBroker.Object,
                this.loggingBroker.Object);
        }

        public static TheoryData<Xeption> LabWorkflowDependencyValidationExceptions()
        {
            string randomErrorMessage = GetRandomString();
            var innerException = new Xeption(randomErrorMessage);

            return new TheoryData<Xeption>
            {
                new LabWorkflowCommandDependencyValidationException(innerException),
                new LabWorkflowCommandValidationException(innerException),
                new CommandDependencyValidationException(innerException),
                new CommandValidationException(innerException),
            };
        }

        public static TheoryData<Xeption> LabWorkflowDependencyExceptions()
        {
            string randomErrorMessage = GetRandomString();
            var innerException = new Xeption(randomErrorMessage);

            return new TheoryData<Xeption>
            {
                new LabWorkflowCommandDependencyException(innerException),
                new LabWorkflowCommandServiceException(innerException),
                new CommandDependencyException(innerException),
                new CommandServiceException(innerException)
            };
        }

        private Expression<Func<Exception, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private Expression<Func<LabWorkflowCommand, bool>> SameLabWorkflowCommandAs(LabWorkflowCommand expectedLabWorkflowCommand)
        {
            return actualLabWorkflowCommand =>
                this.compareLogic.Compare(expectedLabWorkflowCommand, actualLabWorkflowCommand).AreEqual;
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

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
