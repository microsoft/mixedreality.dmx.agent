// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Brokers.DateTimes;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using DMX.Agent.Worker.Services.Foundations.Commands;
using DMX.Agent.Worker.Services.Foundations.LabCommandEvents;
using DMX.Agent.Worker.Services.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Services.Foundations.LabWorkflowEvents;
using System;
using System.Threading.Tasks;

namespace DMX.Agent.Worker.Services.Orchestrations
{
    public class LabWorkflowOrchestrationService : ILabWorkflowOrchestrationService
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

        public ValueTask ProcessLabWorkflow(LabWorkflow workflow) =>
            throw new NotImplementedException();
    }
}
