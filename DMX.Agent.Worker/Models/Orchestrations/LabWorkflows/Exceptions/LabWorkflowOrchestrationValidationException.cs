// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Orchestrations.LabWorkflows
{
    public class LabWorkflowOrchestrationValidationException : Xeption
    {
        public LabWorkflowOrchestrationValidationException(Xeption exception)
            :base(message:"Lab workflow orchestration validation error occurred. Please contact support.",
                 exception)
        { }
    }
}
