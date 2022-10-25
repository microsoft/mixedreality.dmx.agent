// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.LabCommands;
using DMX.Agent.Worker.Models.LabCommands.Exceptions;
using Moq;
using System.Threading;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Azure.ServiceBus;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabCommandEvents
{
    public partial class LabCommandEventServiceTests
    {
        [Fact]
        public void ShouldThrowValidationExceptionOnListenIfLabCommandIsNullAndLogItAsync()
        {
            LabCommand nullLabCommand = null;
            var someLabCommandHandler = new Mock<Func<LabCommand, ValueTask>>();
            var nullLabCommandException = new NullLabCommandException();

            var expectedLabCommandValidationException =
                new LabCommandValidationException(nullLabCommandException);

            Message labCommandMessage =
                CreateLabCommandMessage(nullLabCommand);

            this.queueBrokerMock.Setup(broker =>
                broker.ListenToLabCommandsQueue(It.IsAny<Func<Message, CancellationToken, Task>>()))
                    .Callback<Func<Message, CancellationToken, Task>>(eventFunction =>
                        eventFunction.Invoke(labCommandMessage, It.IsAny<CancellationToken>()));

            // when
            Action listenToLabCommandAction = () =>
                this.labCommandEventService.ListenToLabCommandEvent(
                    someLabCommandHandler.Object);

            // then
            Assert.Throws<LabCommandValidationException>(listenToLabCommandAction);

            this.queueBrokerMock.Verify(broker =>
                broker.ListenToLabCommandsQueue(It.IsAny<Func<Message, CancellationToken, Task>>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedLabCommandValidationException))),
                        Times.Once);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
