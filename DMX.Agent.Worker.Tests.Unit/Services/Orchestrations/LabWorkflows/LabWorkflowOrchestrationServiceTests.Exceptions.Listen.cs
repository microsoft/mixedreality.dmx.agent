// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using DMX.Agent.Worker.Models.Orchestrations.LabWorkflows.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xeptions;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Orchestrations.LabWorkflows
{
    public partial class LabWorkflowOrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(LabWorkflowOrchestrationDependencyValidationExceptions))]
        public void 
            ShouldThrowOrchestrationDependencyValidationExceptionOnListenIfDependencyValidationErrorOccursAndLogItAsync(
                Xeption dependencyValidationException)
        {
            // given
            var expectedLabWorkflowOrchestrationDependencyValidationException = new LabWorkflowOrchestrationDependencyValidationException(
                dependencyValidationException.InnerException as Xeption);

            this.labWorkflowEventServiceMock.Setup(service =>
                service.ListenToLabWorkflowEvent(this.labWorkflowOrchestrationService.ProcessLabWorkflow))
                    .Throws(dependencyValidationException);

            // when
            Action listenLabWorkflowAction =
                () => this.labWorkflowOrchestrationService.ListenToLabWorkflowEvents();

            LabWorkflowOrchestrationDependencyValidationException
                actualLabWorkflowOrchestrationDependencyValidationException =
                    Assert.Throws<LabWorkflowOrchestrationDependencyValidationException>(
                        listenLabWorkflowAction);

            // then
            actualLabWorkflowOrchestrationDependencyValidationException.Should().BeEquivalentTo(
                expectedLabWorkflowOrchestrationDependencyValidationException);
           
            this.labWorkflowEventServiceMock.Verify(service =>
                service.ListenToLabWorkflowEvent(
                    this.labWorkflowOrchestrationService.ProcessLabWorkflow),
                        Times.Once);

            this.loggingBroker.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowOrchestrationDependencyValidationException))),
                        Times.Once);

            this.labWorkflowEventServiceMock.VerifyNoOtherCalls();
            this.labWorkflowCommandServiceMock.VerifyNoOtherCalls();
            this.commandServiceMock.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
            this.dateTimeBroker.VerifyNoOtherCalls();
        }
    }
}
