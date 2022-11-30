// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Azure.Storage.Blobs;
using DMX.Agent.Worker.Models.Configurations;
using Microsoft.Extensions.Configuration;

namespace DMX.Agent.Worker.Brokers.Blobs
{
    public partial class BlobBroker : IBlobBroker
    {
        private readonly BlobServiceClient blobServiceClient;
        private readonly BlobContainerClient blobContainerClient;
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly string containerName;

        public BlobBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = GetStorageConnectionString(this.configuration);
            containerName = GetContainerName(this.configuration);
            blobServiceClient = new BlobServiceClient(connectionString);
            blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        private static string GetStorageConnectionString(IConfiguration configuration)
        {
            LocalConfigurations localConfigurations =
                configuration.Get<LocalConfigurations>();

            return localConfigurations.LabArtifactConfigurations.ConnectionString;
        }

        private static string GetContainerName(IConfiguration configuration)
        {
            LocalConfigurations localConfigurations =
                configuration.Get<LocalConfigurations>();

            return localConfigurations.LabArtifactConfigurations.ContainerName;
        }
    }
}
