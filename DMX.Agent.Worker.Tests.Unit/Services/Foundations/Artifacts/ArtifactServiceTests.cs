// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Brokers.Blobs;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Services.Foundations.Artifacts;
using Moq;
using Tynamix.ObjectFiller;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.Artifacts
{
    public partial class ArtifactServiceTests
    {
        private readonly Mock<IBlobBroker> ArtifactBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IArtifactService ArtifactService;

        public ArtifactServiceTests()
        {
            this.ArtifactBrokerMock = new Mock<IBlobBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.ArtifactService = new ArtifactService(
                this.ArtifactBrokerMock.Object,
                this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();
    }
}
