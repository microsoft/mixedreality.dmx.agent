// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Orchestrations.LabWorkflows
{
    public partial class LabWorkflowOrchestrationServiceTests
    {
        [Fact]
        public void ShouldListenToLabWorkflowEvents()
        {
            // given . when
            this.labWorkflowOrchestrationService.ListenToLabWorkflowEvents();

            // then
            this.labWorkflowEventServiceMock.Verify(service =>
                service.ListenToLabWorkflowEvent(
                    this.labWorkflowOrchestrationService.ProcessLabWorkflow),
                        Times.Once());

            this.labWorkflowEventServiceMock.VerifyNoOtherCalls();
            this.labWorkflowCommandServiceMock.VerifyNoOtherCalls();
            this.commandServiceMock.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
            this.dateTimeBroker.VerifyNoOtherCalls();
        }
    }
}
