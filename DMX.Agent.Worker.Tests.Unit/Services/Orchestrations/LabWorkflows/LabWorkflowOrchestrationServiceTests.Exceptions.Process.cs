// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using DMX.Agent.Worker.Models.Orchestrations.LabWorkflows.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
