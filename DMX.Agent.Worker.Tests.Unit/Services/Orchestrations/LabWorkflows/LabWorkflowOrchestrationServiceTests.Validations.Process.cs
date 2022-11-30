// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using DMX.Agent.Worker.Models.Orchestrations.LabWorkflows;
using FluentAssertions;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Orchestrations.LabWorkflows
{
    public partial class LabWorkflowOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldThrowOrchestrationValidationExceptionIfNullAndLogItAsync()
        {
            // given
            LabWorkflow nullLabWorkflow = null;
            var nullLabWorkflowException = new NullLabWorkflowException();

            var failedLabWorkflowOrchestrationValidationException =
                new FailedLabWorkflowOrchestrationValidationException(
                    nullLabWorkflowException);

            var expectedLabWorkflowOrchestrationValidationException =
                new LabWorkflowOrchestrationValidationException(
                    failedLabWorkflowOrchestrationValidationException);

            // when
            ValueTask processWorkflowTask =
                this.labWorkflowOrchestrationService.ProcessLabWorkflow(nullLabWorkflow);

            LabWorkflowOrchestrationValidationException actualLabWorkflowOrchestrationValidationException =
                await Assert.ThrowsAsync<LabWorkflowOrchestrationValidationException>(
                    processWorkflowTask.AsTask);

            // then
            actualLabWorkflowOrchestrationValidationException.Should().BeEquivalentTo(
                expectedLabWorkflowOrchestrationValidationException);

            this.loggingBroker.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowOrchestrationValidationException))),
                        Times.Once);

            this.commandServiceMock.VerifyNoOtherCalls();
            this.labWorkflowCommandServiceMock.VerifyNoOtherCalls();
            this.labWorkflowEventServiceMock.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
            this.dateTimeBroker.VerifyNoOtherCalls();
        }
    }
}
