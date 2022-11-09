// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using DMX.Agent.Worker.Models.Orchestrations.LabWorkflows.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Orchestrations.LabWorkflows
{
    public partial class LabWorkflowOrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(LabWorkflowDependencyValidationExceptions))]
        public async Task ShouldThrowOrchestrationValidationDependencyExceptionOnProcessIfDependencyValidationErrorOccursAndLogItAsync(
            Xeption dependencyValidationException)
        {
            // given
            LabWorkflow randomLabWorkflow = CreateRandomLabWorkflow();
            LabWorkflow inputLabWorkflow = randomLabWorkflow;

            var failedLabWorkflowOrchestrationDependencyValidationException =
                new FailedLabWorkflowOrchestrationDependencyValidationException(dependencyValidationException);

            var expectedLabWorkflowOrchestrationDependencyValidationException =
                new LabWorkflowOrchestrationDependencyValidationException(
                    failedLabWorkflowOrchestrationDependencyValidationException);

            this.dateTimeBroker.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(dependencyValidationException);

            // when
            ValueTask processLabWorkflowTask =
                this.labWorkflowOrchestrationService.ProcessLabWorkflow(inputLabWorkflow);

            LabWorkflowOrchestrationDependencyValidationException
                actualLabWorkflowOrchestrationDependencyValidationException =
                    await Assert.ThrowsAsync<LabWorkflowOrchestrationDependencyValidationException>(
                        processLabWorkflowTask.AsTask);

            // then
            actualLabWorkflowOrchestrationDependencyValidationException.Should().BeEquivalentTo(
                expectedLabWorkflowOrchestrationDependencyValidationException);

            this.dateTimeBroker.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBroker.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowOrchestrationDependencyValidationException))),
                        Times.Once);

            this.commandServiceMock.VerifyNoOtherCalls();
            this.labWorkflowCommandServiceMock.VerifyNoOtherCalls();
            this.labWorkflowEventServiceMock.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
            this.dateTimeBroker.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(LabWorkflowDependencyExceptions))]
        public async Task ShouldThrowOrchestrationDependencyExceptionOnProcessIfDependencyErrorOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            LabWorkflow randomLabWorkflow = CreateRandomLabWorkflow();
            LabWorkflow inputLabWorkflow = randomLabWorkflow;

            var failedLabWorkflowOrchestrationDependencyException =
                new FailedLabWorkflowOrchestrationDependencyException(dependencyException);

            var expectedLabWorkflowOrchestrationDependencyException =
                new LabWorkflowOrchestrationDependencyException(
                    failedLabWorkflowOrchestrationDependencyException);

            this.dateTimeBroker.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(dependencyException);

            // when
            ValueTask processLabWorkflowTask =
                this.labWorkflowOrchestrationService.ProcessLabWorkflow(inputLabWorkflow);

            LabWorkflowOrchestrationDependencyException
                actualLabWorkflowOrchestrationDependencyException =
                    await Assert.ThrowsAsync<LabWorkflowOrchestrationDependencyException>(
                        processLabWorkflowTask.AsTask);

            // then
            actualLabWorkflowOrchestrationDependencyException.Should().BeEquivalentTo(
                expectedLabWorkflowOrchestrationDependencyException);

            this.dateTimeBroker.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBroker.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowOrchestrationDependencyException))),
                        Times.Once);

            this.commandServiceMock.VerifyNoOtherCalls();
            this.labWorkflowCommandServiceMock.VerifyNoOtherCalls();
            this.labWorkflowEventServiceMock.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
            this.dateTimeBroker.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnProcessIfServiceErrorOccursAndLogItAsync()
        {
            // given
            LabWorkflow randomLabWorkflow = CreateRandomLabWorkflow();
            LabWorkflow inputLabWorkflow = randomLabWorkflow;
            var exception = new Exception();

            var failedLabWorkflowOrchestrationServiceException =
                new FailedLabWorkflowOrchestrationServiceException(exception);

            var expectedLabWorkflowOrchestrationServiceException =
                new LabWorkflowOrchestrationServiceException(
                    failedLabWorkflowOrchestrationServiceException);

            this.dateTimeBroker.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(exception);

            // when
            ValueTask processLabWorkflowTask =
                this.labWorkflowOrchestrationService.ProcessLabWorkflow(inputLabWorkflow);

            LabWorkflowOrchestrationServiceException
                actualLabWorkflowOrchestrationServiceException =
                    await Assert.ThrowsAsync<LabWorkflowOrchestrationServiceException>(
                        processLabWorkflowTask.AsTask);

            // then
            actualLabWorkflowOrchestrationServiceException.Should().BeEquivalentTo(
                expectedLabWorkflowOrchestrationServiceException);

            this.dateTimeBroker.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBroker.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowOrchestrationServiceException))),
                        Times.Once);

            this.commandServiceMock.VerifyNoOtherCalls();
            this.labWorkflowCommandServiceMock.VerifyNoOtherCalls();
            this.labWorkflowEventServiceMock.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
            this.dateTimeBroker.VerifyNoOtherCalls();
        }
    }
}
