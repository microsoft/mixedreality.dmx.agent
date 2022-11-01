// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace DMX.Agent.Worker.Services.Foundations.Commands
{
    public interface ICommandService
    {
        ValueTask<string> ExecuteCommandAsync(string command);
    }
}
