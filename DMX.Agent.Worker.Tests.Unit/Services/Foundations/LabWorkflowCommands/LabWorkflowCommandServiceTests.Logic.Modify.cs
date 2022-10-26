// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabWorkflowCommands
{
    public partial class LabWorkflowCommandServiceTests
    {
        [Fact]
        public async Task ShouldModifyLabWorkflowCommandAsync()
        {
            // given
            LabWorkflowCommand randomLabWorkflowCommand = CreateRandomLabWorkflowCommand();
            LabWorkflowCommand inputLabWorkflowCommand = randomLabWorkflowCommand;
            LabWorkflowCommand updatedLabWorkflowCommand = inputLabWorkflowCommand;
            LabWorkflowCommand expectedLabWorkflowCommand = inputLabWorkflowCommand.DeepClone();

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PutLabWorkflowCommandAsync(inputLabWorkflowCommand))
                    .ReturnsAsync(updatedLabWorkflowCommand);

            // when
            LabWorkflowCommand actualLabWorkflowCommand =
                await this.labWorkflowCommandService.ModifyLabWorkflowCommandAsync(inputLabWorkflowCommand);

            // then
            actualLabWorkflowCommand.Should().BeEquivalentTo(expectedLabWorkflowCommand);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PutLabWorkflowCommandAsync(inputLabWorkflowCommand),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
