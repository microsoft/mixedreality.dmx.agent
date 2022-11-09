// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows.Exceptions;
using FluentAssertions;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;


namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabWorkflowEvents
{
    public partial class LabWorkflowEventServiceTests
    {
        [Fact]
        public void ShouldThrowValidationExceptionOnListenIfLabWorkflowHandlerIsNullAndLogItAsync()
        {
            // given
            Func<LabWorkflow, ValueTask> nullLabWorkflowHandler = null;
            var nullLabWorkflowHandlerException = new NullLabWorkflowHandlerException();

            var expectedLabWorkflowValidationException =
                new LabWorkflowValidationException(nullLabWorkflowHandlerException);

            // when
            Action listenToLabWorkflowAction = () =>
                this.labWorkflowEventService.ListenToLabWorkflowEvent(
                    nullLabWorkflowHandler);

            LabWorkflowValidationException actualLabWorkflowValidationException =
                Assert.Throws<LabWorkflowValidationException>(listenToLabWorkflowAction);

            // then
            actualLabWorkflowValidationException.Should().BeEquivalentTo(
                expectedLabWorkflowValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowValidationException))),
                        Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.ListenToLabWorkflowsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.queueBrokerMock.VerifyNoOtherCalls();
        }
    }
}
