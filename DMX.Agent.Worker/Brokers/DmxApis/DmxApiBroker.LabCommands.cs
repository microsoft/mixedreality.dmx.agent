// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.LabCommands;
using System.Threading.Tasks;
using System;

namespace DMX.Agent.Worker.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabCommandsRelativeUrl = "api/labcommands";

        public async ValueTask<LabCommand> PostLabCommandAsync(LabCommand labCommand) =>
            await PostAsync(LabCommandsRelativeUrl, labCommand);

        public async ValueTask<LabCommand> GetLabCommandByIdAsync(Guid id) =>
            await GetAsync<LabCommand>($"{LabCommandsRelativeUrl}/{id}");
    }
}
