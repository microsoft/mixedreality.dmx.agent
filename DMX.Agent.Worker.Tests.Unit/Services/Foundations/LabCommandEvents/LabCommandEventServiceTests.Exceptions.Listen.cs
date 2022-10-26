// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabCommands;
using DMX.Agent.Worker.Models.Foundations.LabCommands.Exceptions;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabCommandEvents
{
    public partial class LabCommandEventServiceTests
    {
        [Theory]
        [MemberData(nameof(MessageQueueExceptions))]
        public void ShouldThrowCriticalDependencyExceptionOnListenAndLogItAsync(Exception criticalDependencyException)
        {
            // given
            var someLabCommandHandler = new Mock<Func<LabCommand, ValueTask>>();

            var failedLabCommandDependencyException =
                new FailedLabCommandDependencyException(criticalDependencyException);

            var expectedLabCommandDependencyException =
                new LabCommandDependencyException(failedLabCommandDependencyException);

            this.queueBrokerMock.Setup(broker =>
                broker.ListenToLabCommandsQueue(It.IsAny<Func<Message, CancellationToken, Task>>()))
                    .Throws(criticalDependencyException);

            // when
            Action listenToLabCommandAction = () =>
                this.labCommandEventService.ListenToLabCommandEvent(
                    someLabCommandHandler.Object);

            // then
            Assert.Throws<LabCommandDependencyException>(listenToLabCommandAction);

            this.queueBrokerMock.Verify(broker =>
                broker.ListenToLabCommandsQueue(It.IsAny<Func<Message, CancellationToken, Task>>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedLabCommandDependencyException))),
                        Times.Once);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(MessageQueueDependencyExceptions))]
        public void ShouldThrowDependencyExceptionOnListenAndLogItAsync(Exception dependencyException)
        {
            // given
            var someLabCommandHandler = new Mock<Func<LabCommand, ValueTask>>();

            var failedLabCommandDependencyException =
                new FailedLabCommandDependencyException(dependencyException);

            var expectedLabCommandDependencyException =
                new LabCommandDependencyException(failedLabCommandDependencyException);

            this.queueBrokerMock.Setup(broker =>
                broker.ListenToLabCommandsQueue(It.IsAny<Func<Message, CancellationToken, Task>>()))
                    .Throws(dependencyException);

            // when
            Action listenToLabCommandAction = () =>
                this.labCommandEventService.ListenToLabCommandEvent(
                    someLabCommandHandler.Object);

            // then
            Assert.Throws<LabCommandDependencyException>(listenToLabCommandAction);

            this.queueBrokerMock.Verify(broker =>
                broker.ListenToLabCommandsQueue(It.IsAny<Func<Message, CancellationToken, Task>>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandDependencyException))),
                        Times.Once);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionOnListenIfServiceErrorOccurredAndLogIt()
        {
            // given
            var someLabCommandHandler =
                new Mock<Func<LabCommand, ValueTask>>();

            var serviceException = new Exception();

            var failedLabCommandServiceException =
                new FailedLabCommandServiceException(serviceException);

            var expectedLabCommandServiceException =
                new LabCommandServiceException(failedLabCommandServiceException);

            this.queueBrokerMock.Setup(broker =>
                broker.ListenToLabCommandsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()))
                        .Throws(serviceException);

            // when
            Action listenToLabCommandAction = () =>
                this.labCommandEventService.ListenToLabCommandEvent(
                    labCommandEventHandler: someLabCommandHandler.Object);

            // then
            Assert.Throws<LabCommandServiceException>(listenToLabCommandAction);

            this.queueBrokerMock.Verify(broker =>
                broker.ListenToLabCommandsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedLabCommandServiceException))),
                        Times.Once);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
