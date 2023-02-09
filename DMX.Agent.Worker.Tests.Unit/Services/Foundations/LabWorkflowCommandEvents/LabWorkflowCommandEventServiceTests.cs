// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using DMX.Agent.Worker.Brokers.Events;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Services.Foundations.LabWorkflowCommandEvents;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabWorkflowCommandEvents
{
    public partial class LabWorkflowCommandEventServiceTests
    {
        private readonly Mock<IEventBroker> eventBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILabWorkflowCommandEventService labWorkflowCommandEventService;

        public LabWorkflowCommandEventServiceTests()
        {
            this.eventBrokerMock = new Mock<IEventBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.labWorkflowCommandEventService = new LabWorkflowCommandEventService(
                eventBroker: this.eventBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private string GetRandomEventName() =>
            new MnemonicString().GetValue();

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);
    }
}
