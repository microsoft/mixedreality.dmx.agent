// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Brokers.DateTimes;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using DMX.Agent.Worker.Services.Foundations.Commands;
using DMX.Agent.Worker.Services.Foundations.LabCommandEvents;
using DMX.Agent.Worker.Services.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Services.Foundations.LabWorkflowEvents;
using DMX.Agent.Worker.Services.Orchestrations;
using Moq;
using System;
using Tynamix.ObjectFiller;

namespace DMX.Agent.Worker.Tests.Unit.Services.Orchestrations.LabWorkflows
{
    public partial class LabWorkflowOrchestrationServiceTests
    {
        private Mock<ILabWorkflowEventService> labWorkflowEventServiceMock;
        private Mock<ILabWorkflowCommandService> labWorkflowCommandServiceMock;
        private Mock<ICommandService> commandServiceMock;
        private Mock<IDateTimeBroker> dateTimeBroker;
        private Mock<ILoggingBroker> loggingBroker;
        private ILabWorkflowOrchestrationService labWorkflowOrchestrationService;

        public LabWorkflowOrchestrationServiceTests()
        {
        	this.labWorkflowEventServiceMock = new Mock<ILabWorkflowEventService>(MockBehavior.Strict);
        	this.labWorkflowCommandServiceMock = new Mock<ILabWorkflowCommandService>(MockBehavior.Strict);
        	this.commandServiceMock = new Mock<ICommandService>(MockBehavior.Strict);
            this.dateTimeBroker = new Mock<IDateTimeBroker>(MockBehavior.Strict);
            this.loggingBroker = new Mock<ILoggingBroker>(MockBehavior.Strict);

            this.labWorkflowOrchestrationService = new LabWorkflowOrchestrationService(
                this.labWorkflowEventServiceMock.Object,
                this.labWorkflowCommandServiceMock.Object,
                this.commandServiceMock.Object,
                this.dateTimeBroker.Object,
                this.loggingBroker.Object);
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

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
