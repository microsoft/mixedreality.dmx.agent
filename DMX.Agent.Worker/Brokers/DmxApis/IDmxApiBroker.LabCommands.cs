// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabCommands;

namespace DMX.Agent.Worker.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask<LabCommand> PostLabCommandAsync(LabCommand labCommand);
        ValueTask<LabCommand> GetLabCommandByIdAsync(Guid id);
        ValueTask<LabCommand> PutLabCommandAsync(LabCommand labCommand);
    }
}
