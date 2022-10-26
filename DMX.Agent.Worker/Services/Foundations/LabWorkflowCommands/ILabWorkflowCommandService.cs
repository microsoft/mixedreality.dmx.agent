// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowCommands
{
    public interface ILabWorkflowCommandService
    {
        ValueTask<LabWorkflowCommand> ModifyLabWorkflowCommandAsync(LabWorkflowCommand labWorkflowCommand);
    }
}
