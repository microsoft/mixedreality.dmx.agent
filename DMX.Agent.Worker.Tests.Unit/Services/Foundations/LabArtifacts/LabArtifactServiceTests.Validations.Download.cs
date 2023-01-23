// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Azure;
using DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.Artifacts
{
    public partial class LabArtifactServiceTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task ShouldThrowValidationExceptionOnDownloadIfArtifactNameIsNullAndLogItAsync(string invalidString)
        {
            // given
            string nullArtifactName = invalidString;
            string nullFilePath = invalidString;
            
            EmptyLabArtifactNameException emptyArtifactNameException =
                new EmptyLabArtifactNameException();

            LabArtifactValidationException expectedArtifactValidationException =
                new LabArtifactValidationException(emptyArtifactNameException);

            // when
            ValueTask<Response> downloadArtifactTask = 
                this.artifactService.DownloadArtifactAsync(nullArtifactName, nullFilePath);

            LabArtifactValidationException actualArtifactValidationException =
                await Assert.ThrowsAsync<LabArtifactValidationException>(
                    downloadArtifactTask.AsTask);

            // then
            actualArtifactValidationException.Should().BeEquivalentTo(
                expectedArtifactValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtifactValidationException))),
                        Times.Once);

            this.artifactBrokerMock.Verify(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(), 
                    It.IsAny<string>()),
                        Times.Never);

            this.artifactBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
