// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace DMX.Agent.Worker.Brokers.Commands
{
    public partial interface ICommandBroker
    {
        ValueTask<string> RunCommandAsync(string command);
    }
}
