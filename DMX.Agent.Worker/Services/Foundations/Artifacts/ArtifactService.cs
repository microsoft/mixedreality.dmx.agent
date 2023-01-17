// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Azure;
using DMX.Agent.Worker.Brokers.Blobs;
using DMX.Agent.Worker.Brokers.Loggings;
using System;
using System.Threading.Tasks;

namespace DMX.Agent.Worker.Services.Foundations.Artifacts
{
    public class ArtifactService : IArtifactService
    {
        private readonly IBlobBroker artifactBroker;
        private readonly ILoggingBroker loggingBroker;

        public ArtifactService(
            IBlobBroker artifactBroker,
            ILoggingBroker loggingBroker)
        {
            this.artifactBroker = artifactBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Response> DownloadArtifactAsync(string labArtifactName, string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
