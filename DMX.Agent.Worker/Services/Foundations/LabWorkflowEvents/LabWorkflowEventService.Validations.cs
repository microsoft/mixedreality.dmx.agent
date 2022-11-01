// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.LabWorkflows;
using DMX.Agent.Worker.Models.LabWorkflows.Exceptions;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowEvents
{
    public partial class LabWorkflowEventService
    {
        private void ValidateIfEventHandlerIsNull(Func<LabWorkflow, ValueTask> eventHandler)
        {
            if (eventHandler is null)
            {
                throw new NullLabWorkflowHandlerException();
            }
        }
    }
}
