// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Orchestrations.LabWorkflows
{
    public class FailedLabWorkflowOrchestrationValidationException : Xeption
    {
        public FailedLabWorkflowOrchestrationValidationException(Xeption exception)
            : base(message: "Failed lab workflow validation orchestration error occurred. Please try again.",
                 exception)
        { }
    }
}
