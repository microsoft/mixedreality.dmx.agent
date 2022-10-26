// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Xeptions;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabWorkflowCommands
{
    public partial class LabWorkflowCommandServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyException))]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfCriticalErrorOccursAndLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            LabWorkflowCommand someLabWorkflowCommand = CreateRandomLabWorkflowCommand();

            var failedLabWorkflowCommandDependencyException =
                new FailedLabWorkflowCommandDependencyException(criticalDependencyException);

            var expectedLabWorkflowCommandDependencyException =
                new LabWorkflowCommandDependencyException(failedLabWorkflowCommandDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PutLabWorkflowCommandAsync(It.IsAny<LabWorkflowCommand>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<LabWorkflowCommand> modifyLabWorkflowCommandTask =
                this.labWorkflowCommandService.ModifyLabWorkflowCommandAsync(someLabWorkflowCommand);

            LabWorkflowCommandDependencyException actualLabWorkflowCommandDependencyException =
                await Assert.ThrowsAsync<LabWorkflowCommandDependencyException>(
                    modifyLabWorkflowCommandTask.AsTask);

            // then
            actualLabWorkflowCommandDependencyException.Should()
                .BeEquivalentTo(expectedLabWorkflowCommandDependencyException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedLabWorkflowCommandDependencyException))),
                        Times.Once());

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PutLabWorkflowCommandAsync(It.IsAny<LabWorkflowCommand>()),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnModifyIfErrorOccursAndLogItAsync()
        {
            // given
            LabWorkflowCommand someLabWorkflowCommand = CreateRandomLabWorkflowCommand();
            var httpResponseException = new HttpResponseException();

            var failedLabWorkflowCommandDependencyException =
                new FailedLabWorkflowCommandDependencyException(httpResponseException);

            var expectedLabWorkflowCommandDependencyException =
                new LabWorkflowCommandDependencyException(failedLabWorkflowCommandDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PutLabWorkflowCommandAsync(It.IsAny<LabWorkflowCommand>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<LabWorkflowCommand> modifyLabWorkflowCommandTask =
                this.labWorkflowCommandService.ModifyLabWorkflowCommandAsync(someLabWorkflowCommand);

            LabWorkflowCommandDependencyException actualLabWorkflowCommandDependencyException =
                await Assert.ThrowsAsync<LabWorkflowCommandDependencyException>(
                    modifyLabWorkflowCommandTask.AsTask);

            // then
            actualLabWorkflowCommandDependencyException.Should()
                .BeEquivalentTo(expectedLabWorkflowCommandDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PutLabWorkflowCommandAsync(It.IsAny<LabWorkflowCommand>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowCommandDependencyException))),
                        Times.Once());

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyIfErrorOccursAndLogItAsync()
        {
            // given
            LabWorkflowCommand someLabWorkflowCommand = CreateRandomLabWorkflowCommand();
            var serviceException = new Exception();

            var failedLabWorkflowCommandServiceException =
                new FailedLabWorkflowCommandServiceException(serviceException);

            var expectedLabWorkflowCommandServiceException =
                new LabWorkflowCommandServiceException(failedLabWorkflowCommandServiceException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PutLabWorkflowCommandAsync(It.IsAny<LabWorkflowCommand>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<LabWorkflowCommand> modifyLabWorkflowCommandTask =
                this.labWorkflowCommandService.ModifyLabWorkflowCommandAsync(someLabWorkflowCommand);

            LabWorkflowCommandServiceException actualLabWorkflowCommandServiceException =
                await Assert.ThrowsAsync<LabWorkflowCommandServiceException>(
                    modifyLabWorkflowCommandTask.AsTask);

            // then
            actualLabWorkflowCommandServiceException.Should()
                .BeEquivalentTo(expectedLabWorkflowCommandServiceException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PutLabWorkflowCommandAsync(It.IsAny<LabWorkflowCommand>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowCommandServiceException))),
                        Times.Once());

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
