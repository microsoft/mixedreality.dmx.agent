// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.Commands;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.Commands
{
    public partial class CommandServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionAndLogItAsync(Exception dependencyException)
        {
            // given
            string randomString = GetRandomString();
            string inputCommandString = randomString;

            var failedCommandDependencyException =
                new FailedCommandDependencyException(dependencyException);

            var expectedCommandDependencyException =
                new CommandDependencyException(failedCommandDependencyException);

            this.commandBrokerMock.Setup(broker =>
                broker.RunCommandAsync(inputCommandString))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<string> executeCommandAsyncTask =
                this.commandService.ExecuteCommandAsync(inputCommandString);

            CommandDependencyException actualCommandDependencyException =
                await Assert.ThrowsAsync<CommandDependencyException>(
                    executeCommandAsyncTask.AsTask);

            // then
            actualCommandDependencyException.Should().BeEquivalentTo(
                expectedCommandDependencyException);

            this.commandBrokerMock.Verify(broker =>
                broker.RunCommandAsync(It.IsAny<string>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCommandDependencyException))),
                        Times.Once);

            this.commandBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
