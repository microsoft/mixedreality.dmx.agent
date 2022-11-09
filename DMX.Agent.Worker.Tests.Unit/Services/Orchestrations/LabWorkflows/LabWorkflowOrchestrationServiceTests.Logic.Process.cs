// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using Force.DeepCloner;
using Moq;
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
            LabWorkflow expectedLabWorkflow = inputLabWorkflow.DeepClone();
            DateTimeOffset currentStartTime = GetRandomDateTime();
            DateTimeOffset currentCompleteTime = GetRandomDateTime();
            string commandResultString = GetRandomString();
            var mockSequence = new MockSequence();

            List<LabWorkflowCommand> expectedCompletedLabWorkflowCommands =
                expectedLabWorkflow.Commands.DeepClone();

            expectedCompletedLabWorkflowCommands.ForEach(labWorkflowCommand =>
            {
                labWorkflowCommand.Status = CommandStatus.Completed;
                labWorkflowCommand.UpdatedDate = currentCompleteTime;
                labWorkflowCommand.Results = commandResultString;
            });

            inputLabWorkflow.Commands.ForEach(labWorkflowCommand =>
            {
                string labWorkflowCommandArguments = labWorkflowCommand.Arguments;

                LabWorkflowCommand runningLabWorkflowCommand = labWorkflowCommand;
                runningLabWorkflowCommand.Status = CommandStatus.Running;
                runningLabWorkflowCommand.UpdatedDate = currentStartTime;

                LabWorkflowCommand completedLabWorkflowCommand = labWorkflowCommand.DeepClone();
                completedLabWorkflowCommand.Status = CommandStatus.Completed;
                completedLabWorkflowCommand.UpdatedDate = currentCompleteTime;
                completedLabWorkflowCommand.Results = commandResultString;

                this.dateTimeBroker.InSequence(mockSequence).Setup(broker =>
                    broker.GetCurrentDateTimeOffset())
                        .Returns(currentStartTime);

                this.labWorkflowCommandServiceMock.InSequence(mockSequence).Setup(service =>
                    service.ModifyLabWorkflowCommandAsync(It.Is(
                        SameLabWorkflowCommandAs(runningLabWorkflowCommand))))
                            .ReturnsAsync(It.IsAny<LabWorkflowCommand>());

                this.commandServiceMock.InSequence(mockSequence).Setup(service =>
                    service.ExecuteCommandAsync(labWorkflowCommandArguments))
                        .ReturnsAsync(commandResultString);

                this.dateTimeBroker.InSequence(mockSequence).Setup(broker =>
                    broker.GetCurrentDateTimeOffset())
                        .Returns(currentCompleteTime);

                this.labWorkflowCommandServiceMock.InSequence(mockSequence).Setup(service =>
                    service.ModifyLabWorkflowCommandAsync(It.Is(
                        SameLabWorkflowCommandAs(completedLabWorkflowCommand))))
                            .ReturnsAsync(It.IsAny<LabWorkflowCommand>());
            });

            // when
            await this.labWorkflowOrchestrationService.ProcessLabWorkflow(inputLabWorkflow);

            // then
            this.dateTimeBroker.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Exactly(expectedCompletedLabWorkflowCommands.Count * 2));

            expectedCompletedLabWorkflowCommands.ForEach(completedLabWorkflowCommand =>
                this.labWorkflowCommandServiceMock.Verify(service =>
                    service.ModifyLabWorkflowCommandAsync(It.Is(
                        SameLabWorkflowCommandAs(completedLabWorkflowCommand))),
                            Times.Exactly(2)));

            expectedCompletedLabWorkflowCommands.ForEach(labWorkflowCommand =>
                this.commandServiceMock.Verify(service =>
                    service.ExecuteCommandAsync(labWorkflowCommand.Arguments),
                        Times.Once));

            this.dateTimeBroker.VerifyNoOtherCalls();
            this.labWorkflowCommandServiceMock.VerifyNoOtherCalls();
            this.commandServiceMock.VerifyNoOtherCalls();
            this.labWorkflowEventServiceMock.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
        }
    }
}
