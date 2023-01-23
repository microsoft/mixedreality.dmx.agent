// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Azure;
using DMX.Agent.Worker.Brokers.Blobs;
using DMX.Agent.Worker.Brokers.Loggings;
using System.Threading.Tasks;

namespace DMX.Agent.Worker.Services.Foundations.Artifacts
{
    public partial class LabArtifactService : ILabArtifactService
    {
        private readonly IBlobBroker artifactBroker;
        private readonly ILoggingBroker loggingBroker;

        public LabArtifactService(
            IBlobBroker artifactBroker,
            ILoggingBroker loggingBroker)
        {
            this.artifactBroker = artifactBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Response> DownloadArtifactAsync(string labArtifactName, string filePath) =>
            TryCatch(() =>
            {
                ValidateIfStringIsNull(labArtifactName, filePath);

                return this.artifactBroker.DownloadLabArtifactToFilePathAsync(
                    labArtifactName,
                    filePath);
            });
    }
}
