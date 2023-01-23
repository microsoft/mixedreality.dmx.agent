// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Azure;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.Artifacts
{
    public partial class LabArtifactServiceTests
    {
        [Fact]
        public async Task ShouldDownloadArtifactAsync()
        {
            // given
            string someArtifactName = GetRandomString();
            string someFilePath = GetRandomString();
            Response someResponse = GetResponse();
            Response expectedResponse = someResponse.DeepClone();

            this.artifactBrokerMock.Setup(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    It.IsAny<string>(), It.IsAny<string>()))
                        .ReturnsAsync(someResponse);

            // when
            Response actualResponse =
                await this.artifactService.DownloadArtifactAsync(
                    someArtifactName, someFilePath);

            // then
            actualResponse.ToString().Should().BeEquivalentTo(
                expectedResponse.ToString());

            this.artifactBrokerMock.Verify(broker =>
                broker.DownloadLabArtifactToFilePathAsync(
                    someArtifactName,
                    someFilePath),
                        Times.Once);

            this.artifactBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
