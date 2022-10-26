// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;

namespace DMX.Agent.Worker.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabWorkflowCommandsRelativeUrl = "api/labworkflowcommands";

        public async ValueTask<LabWorkflowCommand> PostLabWorkflowCommandAsync(LabWorkflowCommand labWorkflowCommand) =>
            await PostAsync(LabWorkflowCommandsRelativeUrl, labWorkflowCommand);

        public async ValueTask<LabWorkflowCommand> PutLabWorkflowCommandAsync(LabWorkflowCommand labWorkflowCommand) =>
            await PutAsync(LabWorkflowCommandsRelativeUrl, labWorkflowCommand);
    }
}
