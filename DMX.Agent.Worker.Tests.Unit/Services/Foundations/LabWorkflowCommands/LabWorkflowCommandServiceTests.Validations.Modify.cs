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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnModifyIfLabWorkflowCommandIsInvalidAndLogItAsync(
            string invalidString)
        {
            // given
            var invalidLabWorkflowCommand = new LabWorkflowCommand
            {
                Arguments = invalidString,
                Results = invalidString
            };

            var invalidLabWorkflowCommandException = new InvalidLabWorkflowCommandException();

            invalidLabWorkflowCommandException.AddData(
                key: nameof(LabWorkflowCommand.Id),
                values: "Id is required");

            invalidLabWorkflowCommandException.AddData(
                key: nameof(LabWorkflowCommand.LabId),
                values: "Id is required");

            invalidLabWorkflowCommandException.AddData(
                key: nameof(LabWorkflowCommand.WorkflowId),
                values: "Id is required");

            invalidLabWorkflowCommandException.AddData(
                key: nameof(LabWorkflowCommand.Arguments),
                values: "Text is required");

            invalidLabWorkflowCommandException.AddData(
                key: nameof(LabWorkflowCommand.Results),
                values: "Text is required");

            invalidLabWorkflowCommandException.AddData(
                key: nameof(LabWorkflowCommand.CreatedDate),
                values: "Date is required");

            invalidLabWorkflowCommandException.AddData(
                key: nameof(LabWorkflowCommand.UpdatedDate),
                values: "Date is required");

            invalidLabWorkflowCommandException.AddData(
                key: nameof(LabWorkflowCommand.CreatedBy),
                values: "User is required");

            invalidLabWorkflowCommandException.AddData(
                key: nameof(LabWorkflowCommand.UpdatedBy),
                values: "User is required");

            var expectedLabWorkflowCommandValidationException =
                new LabWorkflowCommandValidationException(invalidLabWorkflowCommandException);

            // when
            ValueTask<LabWorkflowCommand> modifyLabWorkflowCommandTask =
                this.labWorkflowCommandService.ModifyLabWorkflowCommandAsync(invalidLabWorkflowCommand);

            LabWorkflowCommandValidationException actualLabWorkflowCommandValidationException =
                await Assert.ThrowsAsync<LabWorkflowCommandValidationException>(
                    modifyLabWorkflowCommandTask.AsTask);

            // then
            actualLabWorkflowCommandValidationException.Should().BeEquivalentTo(
                expectedLabWorkflowCommandValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowCommandValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PutLabWorkflowCommandAsync(
                    It.IsAny<LabWorkflowCommand>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dmxApiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
