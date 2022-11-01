// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Commands;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.Commands
{
    public partial class CommandServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionIfCommandIsNullAndLogItAsync()
        {
            // given
            string randomString = GetRandomString();
            string inputCommandString = randomString;
            var nullCommandException = new NullCommandException();

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
