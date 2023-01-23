// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Azure;

namespace DMX.Agent.Worker.Services.Foundations.Artifacts
{
    public interface ILabArtifactService
    {
        ValueTask<Response> DownloadArtifactAsync(string labArtifactName, string filePath);
    }
}
