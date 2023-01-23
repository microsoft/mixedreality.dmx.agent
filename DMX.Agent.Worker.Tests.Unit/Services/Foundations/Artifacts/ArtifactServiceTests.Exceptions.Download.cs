// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Azure;
using DMX.Agent.Worker.Models.Foundations.Artifacts.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.Artifacts
{
    public partial class ArtifactServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnDownloadAndLogItAsync(
            Exception dependencyException)
        {
            // given
            string someFilePath = GetRandomString();
            string someArtifactName = GetRandomString();

            FailedArtifactDependencyException failedArtifactDependencyException =
                new FailedArtifactDependencyException(dependencyException);

            ArtifactDependencyException expectedArtifactDependencyException =
                new ArtifactDependencyException(failedArtifactDependencyException);

            this.ArtifactBrokerMock.Setup(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(), It.IsAny<string>()))
                        .ThrowsAsync(dependencyException);

            // when
            ValueTask<Response> downloadArtifactTask =
                this.ArtifactService.DownloadArtifactAsync(someArtifactName, someFilePath);

            ArtifactDependencyException actualArtifactDependencyException =
                await Assert.ThrowsAsync<ArtifactDependencyException>(
                    downloadArtifactTask.AsTask);

            // then
            actualArtifactDependencyException.Should().BeEquivalentTo(
                expectedArtifactDependencyException);

            this.ArtifactBrokerMock.Verify(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    someArtifactName, someFilePath),
                        Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtifactDependencyException))),
                        Times.Once());

            this.ArtifactBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData((int)HttpStatusCode.Unauthorized)]
        [InlineData((int)HttpStatusCode.Forbidden)]
        [InlineData((int)HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnDownloadIfCriticalErrorOccursAndLogItAsync(
    int crititicalStatusCode)
        {
            // given
            string randomMessage = GetRandomString();
            string someFilePath = GetRandomString();
            string someArtifactName = GetRandomString();

            var requestFailedException =
                new RequestFailedException(
                    crititicalStatusCode,
                    randomMessage);

            var failedArtifactDependencyException =
                new FailedArtifactDependencyException(requestFailedException);

            var expectedArtifactDependencyException =
                new ArtifactDependencyException(failedArtifactDependencyException);

            this.ArtifactBrokerMock.Setup(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(), It.IsAny<string>()))
                        .Throws(requestFailedException);

            // when
            ValueTask<Response> downloadArtifactTask =
                this.ArtifactService.DownloadArtifactAsync(someArtifactName, someFilePath);

            ArtifactDependencyException actualArtifactDependencyException =
                await Assert.ThrowsAsync<ArtifactDependencyException>(downloadArtifactTask.AsTask);

            // then
            actualArtifactDependencyException.Should().BeEquivalentTo(
                expectedArtifactDependencyException);

            this.ArtifactBrokerMock.Verify(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    someArtifactName, someFilePath),
                        Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedArtifactDependencyException))),
                        Times.Once());

            this.ArtifactBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
