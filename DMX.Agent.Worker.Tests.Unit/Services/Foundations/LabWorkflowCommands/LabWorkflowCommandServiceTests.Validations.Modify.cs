// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabWorkflowCommands
{
    public partial class LabWorkflowCommandServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfLabWorkflowCommandIsNullAndLogItAsync()
        {
            // given
            LabWorkflowCommand nullLabWorkflowCommand = null;

            var nullLabWorkflowCommandException =
                new NullLabWorkflowCommandException();

            var expectedLabWorkflowCommandValidationException =
                new LabWorkflowCommandValidationException(nullLabWorkflowCommandException);

            // when
            ValueTask<LabWorkflowCommand> modifyLabWorkflowCommandTask =
                this.labWorkflowCommandService.ModifyLabWorkflowCommandAsync(nullLabWorkflowCommand);

            LabWorkflowCommandValidationException actualLabWorkflowCommandValidationException =
                await Assert.ThrowsAsync<LabWorkflowCommandValidationException>(
                    modifyLabWorkflowCommandTask.AsTask);

            // then
            actualLabWorkflowCommandValidationException.Should()
                .BeEquivalentTo(expectedLabWorkflowCommandValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowCommandValidationException))),
                        Times.Once());

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PutLabWorkflowCommandAsync(It.IsAny<LabWorkflowCommand>()),
                    Times.Never);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
