// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.LabWorkflows;
using DMX.Agent.Worker.Models.LabWorkflows.Exceptions;
using FluentAssertions;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabWorkflowEvents
{
    public partial class LabWorkflowEventServiceTests
    {
        [Theory]
        [MemberData(nameof(MessageQueueExceptions))]
        public void ShouldThrowCriticalDependencyExceptionOnListenAndLogItAsync(Exception criticalException)
        {
            // given
            var eventHandlerMock = new Mock<Func<LabWorkflow, ValueTask>>();
            LabWorkflow randomLabWorkflow = CreateRandomLabWorkflow();

            var failedLabWorkflowDependencyException =
                new FailedLabWorkflowDependencyException(criticalException);

            var expectedLabWorkflowDependencyException =
                new LabWorkflowDependencyException(failedLabWorkflowDependencyException);

            this.queueBrokerMock.Setup(broker =>
                broker.ListenToLabWorkflowsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()))
                        .Throws(criticalException);

            // when
            Action listenToLabWorkflowEvent = () =>
                this.labWorkflowEventService.ListenToLabWorkflowEvent(
                    eventHandlerMock.Object);

            LabWorkflowDependencyException actualLabWorkflowDependencyException =
                Assert.Throws<LabWorkflowDependencyException>(listenToLabWorkflowEvent);

            // then
            actualLabWorkflowDependencyException.Should().BeEquivalentTo(
                expectedLabWorkflowDependencyException);

            this.queueBrokerMock.Verify(broker =>
                broker.ListenToLabWorkflowsQueue(It.IsAny<Func<Message, CancellationToken, Task>>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedLabWorkflowDependencyException))),
                        Times.Once);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
