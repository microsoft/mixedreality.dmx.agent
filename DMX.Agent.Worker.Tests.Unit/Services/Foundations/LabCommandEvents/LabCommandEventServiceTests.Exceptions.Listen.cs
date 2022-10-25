// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.LabCommands;
using DMX.Agent.Worker.Models.LabCommands.Exceptions;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabCommandEvents
{
    public partial class LabCommandEventServiceTests
    {
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
