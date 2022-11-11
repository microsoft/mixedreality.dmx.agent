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
        [Theory]
        [MemberData(nameof(MessageQueueExceptions))]
        public void ShouldThrowCriticalDependencyExceptionOnListenAndLogIt(Exception criticalException)
        {
            // given
            var eventHandlerMock = new Mock<Func<LabWorkflow, ValueTask>>();

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

        [Theory]
        [MemberData(nameof(MessageQueueDependencyExceptions))]
        public void ShouldThrowDependencyExceptionOnListenAndLogIt(Exception dependencyException)
        {
            // given
            var eventHandlerMock = new Mock<Func<LabWorkflow, ValueTask>>();

            var failedLabWorkflowDependencyException =
                new FailedLabWorkflowDependencyException(dependencyException);

            var expectedLabWorkflowDependencyException =
                new LabWorkflowDependencyException(failedLabWorkflowDependencyException);

            this.queueBrokerMock.Setup(broker =>
                broker.ListenToLabWorkflowsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()))
                        .Throws(dependencyException);

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
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowDependencyException))),
                        Times.Once);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionAndLogIt()
        {
            // given
            var eventHandlerMock = new Mock<Func<LabWorkflow, ValueTask>>();
            var exception = new Exception();

            var failedLabWorkflowServiceException =
                new FailedLabWorkflowServiceException(exception);

            var expectedLabWorkflowServiceException =
                new LabWorkflowServiceException(failedLabWorkflowServiceException);

            this.queueBrokerMock.Setup(broker =>
                broker.ListenToLabWorkflowsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()))
                        .Throws(exception);

            // when
            Action listenToLabWorkflowEvent = () =>
                this.labWorkflowEventService.ListenToLabWorkflowEvent(
                    eventHandlerMock.Object);

            LabWorkflowServiceException actualLabWorkflowServiceException =
                Assert.Throws<LabWorkflowServiceException>(listenToLabWorkflowEvent);

            // then
            actualLabWorkflowServiceException.Should().BeEquivalentTo(
                expectedLabWorkflowServiceException);

            this.queueBrokerMock.Verify(broker =>
                broker.ListenToLabWorkflowsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowServiceException))),
                        Times.Once);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
