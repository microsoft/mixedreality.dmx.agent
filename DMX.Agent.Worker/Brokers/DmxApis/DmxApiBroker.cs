// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.Clients;

namespace DMX.Agent.Worker.Brokers.DmxApis
{
    public partial class DmxApiBroker : IDmxApiBroker
    {
        private HttpClient httpClient;
        private readonly IRESTFulApiFactoryClient apiClient;

        public DmxApiBroker(string baseUrl)
        {
            this.httpClient = new HttpClient();
            this.apiClient = GetApiClient(baseUrl);
        }

        private async ValueTask<T> PostAsync<T>(string relativeUrl, T content) =>
            await this.apiClient.PostContentAsync<T>(relativeUrl, content);

        private async ValueTask<T> GetAsync<T>(string relativeUrl) =>
            await this.apiClient.GetContentAsync<T>(relativeUrl);

        private IRESTFulApiFactoryClient GetApiClient(string baseUrl)
        {
            this.httpClient.BaseAddress = new Uri(baseUrl);

            return new RESTFulApiFactoryClient(this.httpClient);
        }
    }
}
