// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabCommands;
using DMX.Agent.Worker.Models.Foundations.LabCommands.Exceptions;
using FluentAssertions;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabCommandEvents
{
    public partial class LabCommandEventServiceTests
    {
        [Fact]
        public void ShouldThrowValidationExceptionOnListenIfLabCommandHandlerIsNullAndLogIt()
        {
            // given
            Func<LabCommand, ValueTask> nullLabCommandHandler = null;
            var nullLabCommandHandlerException = new NullLabCommandHandlerException();

            var expectedLabCommandValidationException =
                new LabCommandValidationException(nullLabCommandHandlerException);

            // when
            Action listenToLabCommandAction = () =>
                this.labCommandEventService.ListenToLabCommandEvent(
                    nullLabCommandHandler);

            LabCommandValidationException actualLabCommandValidationException =
                Assert.Throws<LabCommandValidationException>(listenToLabCommandAction);

            // then
            actualLabCommandValidationException.Should().BeEquivalentTo(
                expectedLabCommandValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandValidationException))),
                        Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.ListenToLabCommandsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()),
                        Times.Never);

            this.queueBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
