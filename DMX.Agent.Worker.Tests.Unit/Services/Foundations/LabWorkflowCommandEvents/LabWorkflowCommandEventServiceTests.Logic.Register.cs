// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabWorkflowCommandEvents
{
    public partial class LabWorkflowCommandEventServiceTests
    {
        [Fact]
        public void ShouldRegisterLabWorkflowEventHandler()
        {
            // given
            var labWorkflowCommandEventHandlerMock =
                new Mock<Func<LabWorkflowCommand, ValueTask>>();

            string randomEventName = GetRandomEventName();
            string inputEventName = randomEventName;

            // when
            this.labWorkflowCommandEventService.RegisterLabWorkflowCommandEventHandler(
                labWorkflowCommandEventHandler: labWorkflowCommandEventHandlerMock.Object,
                eventName: inputEventName);

            // then
            this.eventBrokerMock.Verify(broker =>
                broker.ListenToLabWorkflowCommandEvent(
                    labWorkflowCommandEventHandlerMock.Object,
                    inputEventName),
                        Times.Once);

            this.eventBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
