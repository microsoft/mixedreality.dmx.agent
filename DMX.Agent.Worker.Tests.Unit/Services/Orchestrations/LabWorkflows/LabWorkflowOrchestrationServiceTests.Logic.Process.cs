// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Orchestrations.LabWorkflows
{
    public partial class LabWorkflowOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldProcessLabWorkflowAsync()
        {
            // given
            LabWorkflow randomLabWorkflow = CreateRandomLabWorkflow();
            LabWorkflow inputLabWorkflow = randomLabWorkflow;
            string randomString = GetRandomString();
            string commandResultString = randomString;

            MockSequence mockSequence = new MockSequence();

            this.commandServiceMock.InSequence(mockSequence).Setup(service =>
                service.ExecuteCommandAsync(It.IsAny<string>()))
                    .ReturnsAsync(randomString);

            this.labWorkflowCommandServiceMock.InSequence(mockSequence).Setup(service =>
                service.ModifyLabWorkflowCommandAsync(It.IsAny<LabWorkflowCommand>()))
                    .ReturnsAsync(It.IsAny<LabWorkflowCommand>());


            // when
            await this.labWorkflowOrchestrationService.ProcessLabWorkflow(inputLabWorkflow);

            // then

            foreach (LabWorkflowCommand labWorkflowCommand in inputLabWorkflow.Commands)
            {
                LabWorkflowCommand startedLabWorkflowCommand = labWorkflowCommand;
                startedLabWorkflowCommand.Status = CommandStatus.Running;

                this.labWorkflowCommandServiceMock.Verify(service =>
                    service.ModifyLabWorkflowCommandAsync(startedLabWorkflowCommand),
                        Times.Once);

            }
        }
    }
}
