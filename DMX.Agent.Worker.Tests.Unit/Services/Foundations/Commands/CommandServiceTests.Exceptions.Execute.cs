// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.Commands;
using DMX.Agent.Worker.Models.Foundations.Commands.Exceptions;
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
        public async Task ShouldThrowDependencyExceptionAndLogItAsync(
            Exception dependencyException)
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

        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionIfDependencyValidationErrorOccursAndLogItAsync(
            Exception dependencyValidationException)
        {
            // given
            string randomString = GetRandomString();
            string inputCommandString = randomString;

            var failedCommandDependencyValidationException =
                new FailedCommandDependencyValidationException(dependencyValidationException);

            var expectedCommandDependencyValidationException = 
                new CommandDependencyValidationException(failedCommandDependencyValidationException);

            this.commandBrokerMock.Setup(broker =>
                broker.RunCommandAsync(It.IsAny<string>()))
                    .ThrowsAsync(dependencyValidationException);

            // when
            ValueTask<string> executeCommandTask =
                this.commandService.ExecuteCommandAsync(inputCommandString);

            CommandDependencyValidationException actualCommandDependencyValidationException =
                await Assert.ThrowsAsync<CommandDependencyValidationException>(
                    executeCommandTask.AsTask);

            // then
            actualCommandDependencyValidationException.Should().BeEquivalentTo(
                expectedCommandDependencyValidationException);

            this.commandBrokerMock.Verify(broker =>
                broker.RunCommandAsync(It.IsAny<string>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCommandDependencyValidationException))),
                        Times.Once);

            this.commandBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string randomString = GetRandomString();
            string inputCommandString = randomString;
            var exception = new Exception();

            var failedCommandServiceException =
                new FailedCommandServiceException(exception);

            var expectedCommandServiceException =
                new CommandServiceException(failedCommandServiceException);

            this.commandBrokerMock.Setup(broker =>
                broker.RunCommandAsync(It.IsAny<string>()))
                    .ThrowsAsync(exception);

            // when
            ValueTask<string> executeCommandTask =
                this.commandService.ExecuteCommandAsync(inputCommandString);

            CommandServiceException actualCommandServiceException =
                await Assert.ThrowsAsync<CommandServiceException>(
                    executeCommandTask.AsTask);

            // then
            actualCommandServiceException.Should().BeEquivalentTo(
                expectedCommandServiceException);

            this.commandBrokerMock.Verify(broker =>
                broker.RunCommandAsync(It.IsAny<string>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCommandServiceException))),
                        Times.Once);

            this.commandBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
