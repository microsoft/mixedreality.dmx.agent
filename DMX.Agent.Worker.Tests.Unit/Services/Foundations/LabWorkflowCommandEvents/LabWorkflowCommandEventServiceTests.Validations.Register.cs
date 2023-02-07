// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommandEvents;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using FluentAssertions;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabWorkflowCommandEvents
{
    public partial class LabWorkflowCommandEventServiceTests
    {
        [Fact]
        public void ShouldThrowValidationExceptionOnRegisterIfLabWorkflowCommandEventHandlerIsNullAndLogItAsync()
        {
            // given
            Func<LabWorkflowCommand, ValueTask> nullLabWorkflowCommandEventHandler = null;
            string randomEventName = GetRandomEventName();
            string inputEventName = randomEventName;

            var nullLabWorkflowCommandEventHandlerException =
                new NullLabWorkflowCommandEventHandlerException();

            var expectedLabWorkflowCommandEventValidationException =
                new LabWorkflowCommandEventValidationException(
                    nullLabWorkflowCommandEventHandlerException);

            // when
            void registerLabWorkflowCommandEventAction() =>
                this.labWorkflowCommandEventService.RegisterLabWorkflowCommandEventHandler(
                    nullLabWorkflowCommandEventHandler,
                    inputEventName);

            LabWorkflowCommandEventValidationException actualLabWorkflowCommandEventValidationException =
                Assert.Throws<LabWorkflowCommandEventValidationException>(
                    registerLabWorkflowCommandEventAction);

            // then
            actualLabWorkflowCommandEventValidationException.Should().BeEquivalentTo(
                expectedLabWorkflowCommandEventValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(
                    expectedLabWorkflowCommandEventValidationException),
                        Times.Once);

            this.eventBrokerMock.Verify(broker =>
                broker.ListenToLabWorkflowCommandEvent(
                    nullLabWorkflowCommandEventHandler,
                    inputEventName),
                        Times.Never);

            this.eventBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
