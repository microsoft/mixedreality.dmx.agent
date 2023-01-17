﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Azure;
using DMX.Agent.Worker.Models.Foundations.Artifacts.Exceptions;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.Artifacts
{
    public partial class ArtifactServiceTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task ShouldThrowValidationExceptionOnDownloadIfArtifactNameIsNullAndLogItAsync(string invalidString)
        {
            // given
            string nullArtifactName = invalidString;
            string someFilePath = GetRandomString();
            
            EmptyArtifactNameException emptyArtifactNameException =
                new EmptyArtifactNameException();

            ArtifactValidationException expectedArtifactValidationException =
                new ArtifactValidationException(emptyArtifactNameException);

            // when
            ValueTask<Response> downloadArtifactTask = 
                this.ArtifactService.DownloadArtifactAsync(nullArtifactName, someFilePath);

            ArtifactValidationException actualArtifactValidationException =
                await Assert.ThrowsAsync<ArtifactValidationException>(
                    downloadArtifactTask.AsTask);

            // then
            actualArtifactValidationException.Should().BeEquivalentTo(
                expectedArtifactValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtifactValidationException))),
                        Times.Once);

            this.ArtifactBrokerMock.Verify(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(), 
                    It.IsAny<string>()),
                        Times.Never);

            this.ArtifactBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task ShouldThrowValidationExceptionOnDownloadIfArtifactFilePathIsNullAndLogItAsync(string invalidString)
        {
            // given
            string someArtifactName = GetRandomString();
            string nullFilePath = invalidString;

            EmptyArtifactFilePathException emptyArtifactFilePathException =
                new EmptyArtifactFilePathException();

            ArtifactValidationException expectedArtifactValidationException =
                new ArtifactValidationException(emptyArtifactFilePathException);

            // when
            ValueTask<Response> downloadArtifactTask =
                this.ArtifactService.DownloadArtifactAsync(someArtifactName, nullFilePath);

            ArtifactValidationException actualArtifactValidationException =
                await Assert.ThrowsAsync<ArtifactValidationException>(
                    downloadArtifactTask.AsTask);

            // then
            actualArtifactValidationException.Should().BeEquivalentTo(
                expectedArtifactValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtifactValidationException))),
                        Times.Once);

            this.ArtifactBrokerMock.Verify(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Never);

            this.ArtifactBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
