// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Brokers.DateTimes;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using DMX.Agent.Worker.Services.Foundations.Commands;
using DMX.Agent.Worker.Services.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Services.Foundations.LabWorkflowEvents;

namespace DMX.Agent.Worker.Services.Orchestrations.LabWorkflows
{
    public partial class LabWorkflowOrchestrationService : ILabWorkflowOrchestrationService
    {
        private ILabWorkflowEventService labWorkflowEventService;
        private ILabWorkflowCommandService labWorkflowCommandService;
        private ICommandService commandService;
        private IDateTimeBroker dateTimeBroker;
        private ILoggingBroker loggingBroker;

        public LabWorkflowOrchestrationService(
            ILabWorkflowEventService labWorkflowEventService,
            ILabWorkflowCommandService labWorkflowCommandService,
            ICommandService commandService,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.labWorkflowEventService = labWorkflowEventService;
            this.labWorkflowCommandService = labWorkflowCommandService;
            this.commandService = commandService;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public void ListenToLabWorkflowEvents() =>
        TryCatch(() =>
        {
            this.labWorkflowEventService.ListenToLabWorkflowEvent(
                ProcessLabWorkflow);
        });

        public ValueTask ProcessLabWorkflow(LabWorkflow labWorkflow) =>
        TryCatch(async () =>
        {
            ValidateIfLabWorkflowIsNotNull(labWorkflow);

            foreach (LabWorkflowCommand command in labWorkflow.Commands)
            {
                command.UpdatedDate = this.dateTimeBroker.GetCurrentDateTimeOffset();
                command.Status = CommandStatus.Running;
                await this.labWorkflowCommandService.ModifyLabWorkflowCommandAsync(command);

                var result = await this.commandService.ExecuteCommandAsync(command.Arguments);

                command.Results = result;
                command.UpdatedDate = this.dateTimeBroker.GetCurrentDateTimeOffset();
                command.Status = CommandStatus.Completed;
                await this.labWorkflowCommandService.ModifyLabWorkflowCommandAsync(command);
            }
        });
    }
}
