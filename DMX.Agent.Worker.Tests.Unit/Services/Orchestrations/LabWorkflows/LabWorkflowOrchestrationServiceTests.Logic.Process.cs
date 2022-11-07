// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using Moq;
using System;
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
            MockSequence mockSequence = new MockSequence();

            foreach (LabWorkflowCommand labWorkflowCommand in inputLabWorkflow.Commands)
            {
                DateTimeOffset currentStartTime = DateTime.UtcNow;
                dateTimeBroker.InSequence(mockSequence).Setup(broker =>
                    broker.GetCurrentDateTimeOffset())
                        .Returns(currentStartTime);

                LabWorkflowCommand startedLabWorkflowCommand = labWorkflowCommand;
                startedLabWorkflowCommand.Status = CommandStatus.Running;
                startedLabWorkflowCommand.UpdatedDate = currentStartTime;

                this.labWorkflowCommandServiceMock.InSequence(mockSequence).Setup(service =>
                    service.ModifyLabWorkflowCommandAsync(startedLabWorkflowCommand))
                        .ReturnsAsync(It.IsAny<LabWorkflowCommand>());

                string labWorkflowCommandString = labWorkflowCommand.Arguments;
                string randomString = GetRandomString();
                string commandResultString = randomString;

                this.commandServiceMock.InSequence(mockSequence).Setup(service =>
                    service.ExecuteCommandAsync(labWorkflowCommandString))
                        .ReturnsAsync(randomString);

                DateTimeOffset currentCompleteTime = DateTime.UtcNow;
                dateTimeBroker.InSequence(mockSequence).Setup(broker =>
                    broker.GetCurrentDateTimeOffset())
                        .Returns(currentCompleteTime);

                LabWorkflowCommand completedLabWorkflowCommand = labWorkflowCommand;
                completedLabWorkflowCommand.Status = CommandStatus.Completed;
                completedLabWorkflowCommand.UpdatedDate = currentCompleteTime;
                completedLabWorkflowCommand.Results = commandResultString;

                this.labWorkflowCommandServiceMock.InSequence(mockSequence).Setup(service =>
                    service.ModifyLabWorkflowCommandAsync(completedLabWorkflowCommand))
                        .ReturnsAsync(It.IsAny<LabWorkflowCommand>());
            }

            // when
            await this.labWorkflowOrchestrationService.ProcessLabWorkflow(inputLabWorkflow);

            // then
            this.dateTimeBroker.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Exactly(inputLabWorkflow.Commands.Count * 2));

            this.labWorkflowCommandServiceMock.Verify(service =>
                service.ModifyLabWorkflowCommandAsync(It.IsAny<LabWorkflowCommand>()),
                    Times.Exactly(inputLabWorkflow.Commands.Count * 2));

            this.commandServiceMock.Verify(service =>
                service.ExecuteCommandAsync(It.IsAny<string>()),
                    Times.Exactly(inputLabWorkflow.Commands.Count));

            this.dateTimeBroker.VerifyNoOtherCalls();
            this.labWorkflowCommandServiceMock.VerifyNoOtherCalls();
            this.commandServiceMock.VerifyNoOtherCalls();
            this.labWorkflowEventServiceMock.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
        }
    }
}
