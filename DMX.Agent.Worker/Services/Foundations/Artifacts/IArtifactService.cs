// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Azure;
using System.Threading.Tasks;

namespace DMX.Agent.Worker.Services.Foundations.Artifacts
{
    public interface IArtifactService
    {
        ValueTask<Response> DownloadArtifactAsync(string labArtifactName, string filePath);
    }
}
