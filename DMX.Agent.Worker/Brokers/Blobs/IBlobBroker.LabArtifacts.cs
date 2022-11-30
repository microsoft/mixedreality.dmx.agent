// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Azure;

namespace DMX.Agent.Worker.Brokers.Blobs
{
    public partial interface IBlobBroker
    {
        ValueTask<Response> DownloadLabArtifactToFilePathAsync(string labArtifactName, string filePath);
    }
}
