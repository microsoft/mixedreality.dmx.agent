// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.Commands.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.Commands
{
    public partial class CommandServiceTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task ShouldThrowValidationExceptionIfCommandIsNullAndLogItAsync(string emptyInputString)
        {
            // given
            string inputCommandString = emptyInputString;
            var nullCommandException = new EmptyCommandException();

            var expectedCommandValidationException =
                new CommandValidationException(nullCommandException);

            // when
            ValueTask<string> actualResponseStringTask =
                this.commandService.ExecuteCommandAsync(inputCommandString);

            CommandValidationException actualCommandValidationException =
                await Assert.ThrowsAsync<CommandValidationException>(
                    actualResponseStringTask.AsTask);

            // then
            actualCommandValidationException.Should().BeEquivalentTo(
                expectedCommandValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCommandValidationException))),
                        Times.Once);

            this.commandBrokerMock.Verify(broker =>
                broker.RunCommandAsync(It.IsAny<string>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.commandBrokerMock.VerifyNoOtherCalls();
        }
    }
}
