// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;

namespace DMX.Agent.Worker.Brokers.Blobs
{
    public partial class BlobBroker : IBlobBroker
    {
        public async ValueTask<Response> DownloadLabArtifactToFilePathAsync(
            string labArtifactName,
            string filePath)
        {
            BlobClient blobClient =
                blobContainerClient.GetBlobClient(labArtifactName);

            return await blobClient.DownloadToAsync(filePath);
        }
    }
}
