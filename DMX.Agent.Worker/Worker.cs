// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using DMX.Agent.Worker.Brokers.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DMX.Agent.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IBlobBroker blobBroker;
        private readonly IConfiguration configuration;


        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.blobBroker = new BlobBroker(configuration);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
            throw new NotImplementedException();
    }
}