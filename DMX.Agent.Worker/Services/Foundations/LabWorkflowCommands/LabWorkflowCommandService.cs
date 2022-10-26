// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Brokers.DmxApis;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowCommands
{
    public partial class LabWorkflowCommandService : ILabWorkflowCommandService
    {
        private readonly IDmxApiBroker dmxApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public LabWorkflowCommandService(
            IDmxApiBroker dmxApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.dmxApiBroker = dmxApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<LabWorkflowCommand> ModifyLabWorkflowCommandAsync(LabWorkflowCommand labWorkflowCommand) =>
        TryCatch(async () =>
        {
            ValidateLabWorkflowCommandIsNotNull(labWorkflowCommand);

            return await this.dmxApiBroker.PutLabWorkflowCommandAsync(labWorkflowCommand);
        });
    }
}
