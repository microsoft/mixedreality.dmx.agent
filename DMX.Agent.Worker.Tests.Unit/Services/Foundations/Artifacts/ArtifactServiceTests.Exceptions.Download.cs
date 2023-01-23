// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Azure;
using DMX.Agent.Worker.Models.Foundations.Artifacts.Exceptions;
using DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions;
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

            this.artifactBrokerMock.Setup(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(), It.IsAny<string>()))
                        .ThrowsAsync(dependencyException);

            // when
            ValueTask<Response> downloadArtifactTask =
                this.artifactService.DownloadArtifactAsync(someArtifactName, someFilePath);

            ArtifactDependencyException actualArtifactDependencyException =
                await Assert.ThrowsAsync<ArtifactDependencyException>(
                    downloadArtifactTask.AsTask);

            // then
            actualArtifactDependencyException.Should().BeEquivalentTo(
                expectedArtifactDependencyException);

            this.artifactBrokerMock.Verify(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    someArtifactName, someFilePath),
                        Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtifactDependencyException))),
                        Times.Once());

            this.artifactBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData((int)HttpStatusCode.Unauthorized)]
        [InlineData((int)HttpStatusCode.Forbidden)]
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

            this.artifactBrokerMock.Setup(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(), It.IsAny<string>()))
                        .Throws(requestFailedException);

            // when
            ValueTask<Response> downloadArtifactTask =
                this.artifactService.DownloadArtifactAsync(someArtifactName, someFilePath);

            ArtifactDependencyException actualArtifactDependencyException =
                await Assert.ThrowsAsync<ArtifactDependencyException>(downloadArtifactTask.AsTask);

            // then
            actualArtifactDependencyException.Should().BeEquivalentTo(
                expectedArtifactDependencyException);

            this.artifactBrokerMock.Verify(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    someArtifactName, someFilePath),
                        Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedArtifactDependencyException))),
                        Times.Once());

            this.artifactBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData((int)HttpStatusCode.NotFound)]
        public async Task ShouldThrowDependencyValidationExceptionOnDownloadIfLabArtifactNotFoundErrorOccursAndLogItAsync(
            int notFoundStatusCode)
        {
            // given
            string randomMessage = GetRandomString();
            string someFilePath = GetRandomString();
            string someArtifactName = GetRandomString();

            var requestFailedException =
                new RequestFailedException(
                    notFoundStatusCode,
                    randomMessage);

            var notFoundLabArtifactException =
                new NotFoundLabArtifactException(requestFailedException);

            var expectedLabArtifactDependencyValidationException =
                new LabArtifactDependencyValidationException(notFoundLabArtifactException);

            this.artifactBrokerMock.Setup(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(), It.IsAny<string>()))
                        .Throws(requestFailedException);

            // when
            ValueTask<Response> downloadArtifactTask =
                this.artifactService.DownloadArtifactAsync(
                    someArtifactName, someFilePath);

            LabArtifactDependencyValidationException actualLabArtifactDependencyValidationException =
                await Assert.ThrowsAsync<LabArtifactDependencyValidationException>(
                    downloadArtifactTask.AsTask);

            // then
            actualLabArtifactDependencyValidationException.Should().BeEquivalentTo(
                expectedLabArtifactDependencyValidationException);

            this.artifactBrokerMock.Verify(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(), It.IsAny<string>()),
                        Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabArtifactDependencyValidationException))),
                        Times.Once());

            this.artifactBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnDownloadIfPathIsNotAccessibleErrorOccursAndLogItAsync()
        {
            // given
            string randomMessage = GetRandomString();
            string someFilePath = GetRandomString();
            string someArtifactName = GetRandomString();

            var requestFailedException =
                new UnauthorizedAccessException(randomMessage);

            var notFoundLabArtifactException =
                new LabArtifactFilePathUnauthorizedException(requestFailedException);

            var expectedLabArtifactDependencyValidationException =
                new LabArtifactDependencyValidationException(notFoundLabArtifactException);

            this.artifactBrokerMock.Setup(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(), It.IsAny<string>()))
                        .Throws(requestFailedException);

            // when
            ValueTask<Response> downloadArtifactTask =
                this.artifactService.DownloadArtifactAsync(
                    someArtifactName, someFilePath);

            LabArtifactDependencyValidationException actualLabArtifactDependencyValidationException =
                await Assert.ThrowsAsync<LabArtifactDependencyValidationException>(
                    downloadArtifactTask.AsTask);

            // then
            actualLabArtifactDependencyValidationException.Should().BeEquivalentTo(
                expectedLabArtifactDependencyValidationException);

            this.artifactBrokerMock.Verify(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(), It.IsAny<string>()),
                        Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabArtifactDependencyValidationException))),
                        Times.Once());

            this.artifactBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnDownloadIfErrorOccursAndLogItAsync()
        {
            // given
            string someFilePath = GetRandomString();
            string someArtifactName = GetRandomString();

            var exception = new Exception();

            var failedLabArtifactServiceException =
                new FailedLabArtifactServiceException(
                    exception);

            var expectedLabArtifactServiceException =
                new LabArtifactServiceException(
                    failedLabArtifactServiceException);

            this.artifactBrokerMock.Setup(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(), It.IsAny<string>()))
                        .Throws(exception);

            // when
            ValueTask<Response> downloadArtifactTask =
                this.artifactService.DownloadArtifactAsync(
                    someArtifactName, someFilePath);

            LabArtifactServiceException actualLabArtifactServiceException =
                await Assert.ThrowsAsync<LabArtifactServiceException>(
                    downloadArtifactTask.AsTask);

            // then
            actualLabArtifactServiceException.Should().BeEquivalentTo(
                expectedLabArtifactServiceException);

            this.artifactBrokerMock.Verify(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(), It.IsAny<string>()),
                        Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabArtifactServiceException))),
                        Times.Once());

            this.artifactBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
