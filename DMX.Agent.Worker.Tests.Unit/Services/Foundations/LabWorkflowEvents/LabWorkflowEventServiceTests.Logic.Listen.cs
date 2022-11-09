// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabWorkflowEvents
{
    public partial class LabWorkflowEventServiceTests
    {
        [Fact]
        public void ShouldListenToLabWorkflowEvent()
        {
            // given
            var eventHandlerMock = new Mock<Func<LabWorkflow, ValueTask>>();

            LabWorkflow randomLabWorkflow = CreateRandomLabWorkflow();
            LabWorkflow inputLabWorkflow = randomLabWorkflow;

            Message labWorkflowMessage = CreateLabWorkflowMessage(inputLabWorkflow);

            this.queueBrokerMock.Setup(broker =>
                broker.ListenToLabWorkflowsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()))
                        .Callback<Func<Message, CancellationToken, Task>>(eventFunction =>
                            eventFunction.Invoke(labWorkflowMessage, It.IsAny<CancellationToken>()));

            // when
            labWorkflowEventService.ListenToLabWorkflowEvent(
                labWorkflowEventHandler: eventHandlerMock.Object);

            // then
            eventHandlerMock.Verify(handler =>
                handler.Invoke(It.Is(SameLabWorkflowAs(
                    inputLabWorkflow))),
                        Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.ListenToLabWorkflowsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()),
                        Times.Once);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
