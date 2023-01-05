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
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly string containerName;
        private readonly BlobServiceClient blobServiceClient;
        private readonly BlobContainerClient blobContainerClient;

        public BlobBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = GetStorageConnectionString(this.configuration);
            this.containerName = GetContainerName(this.configuration);
            this.blobServiceClient = new BlobServiceClient(connectionString);
            this.blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
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
