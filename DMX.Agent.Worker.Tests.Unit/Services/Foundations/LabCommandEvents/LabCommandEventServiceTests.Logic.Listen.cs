// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabCommands;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabCommandEvents
{
    public partial class LabCommandEventServiceTests
    {
        [Fact]
        public void ShouldListenToLabCommandEvent()
        {
            // given
            var labCommandEventHandlerMock =
                new Mock<Func<LabCommand, ValueTask>>();

            LabCommand randomLabCommand = CreateRandomLabCommand();
            LabCommand incomingLabCommand = randomLabCommand;

            Message labCommandMessage =
                CreateLabCommandMessage(incomingLabCommand);

            this.queueBrokerMock.Setup(broker =>
                broker.ListenToLabCommandsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()))
                        .Callback<Func<Message, CancellationToken, Task>>(eventFunction =>
                            eventFunction.Invoke(labCommandMessage, It.IsAny<CancellationToken>()));

            // when
            labCommandEventService.ListenToLabCommandEvent(
                labCommandEventHandler: labCommandEventHandlerMock.Object);

            // then
            labCommandEventHandlerMock.Verify(handler =>
                handler.Invoke(It.Is(SameLabCommandAs(incomingLabCommand))),
                    Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.ListenToLabCommandsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()),
                        Times.Once);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
