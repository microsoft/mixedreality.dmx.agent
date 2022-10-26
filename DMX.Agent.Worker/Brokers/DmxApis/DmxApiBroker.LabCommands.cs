// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabCommands;

namespace DMX.Agent.Worker.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabCommandsRelativeUrl = "api/labcommands";

        public async ValueTask<LabCommand> PostLabCommandAsync(LabCommand labCommand) =>
            await PostAsync(LabCommandsRelativeUrl, labCommand);

        public async ValueTask<LabCommand> GetLabCommandByIdAsync(Guid id) =>
            await GetAsync<LabCommand>($"{LabCommandsRelativeUrl}/{id}");

        public async ValueTask<LabCommand> PutLabCommandAsync(LabCommand labCommand) =>
            await PutAsync(LabCommandsRelativeUrl, labCommand);
    }
}
