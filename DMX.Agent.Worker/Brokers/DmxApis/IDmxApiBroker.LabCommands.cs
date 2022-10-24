// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.LabCommands;
using System.Threading.Tasks;
using System;

namespace DMX.Agent.Worker.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask<LabCommand> PostLabCommandAsync(LabCommand labCommand);
        ValueTask<LabCommand> GetLabCommandByIdAsync(Guid id);
    }
}
