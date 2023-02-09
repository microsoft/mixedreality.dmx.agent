// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using System.Threading.Tasks;
using System;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommandEvents;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowCommandEvents
{
    public partial class LabWorkflowCommandEventService
    {
        private void ValidateLabWorkflowCommandEventHandler(
            Func<LabWorkflowCommand, ValueTask> labWorkflowCommandEventHandler)
        {
            if (labWorkflowCommandEventHandler is null)
            {
                throw new NullLabWorkflowCommandEventHandlerException();
            }
        }
    }
}
