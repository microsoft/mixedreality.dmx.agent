// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Orchestrations.LabWorkflows.Exceptions
{
    public class FailedLabWorkflowOrchestrationDependencyValidationException : Xeption
    {
        public FailedLabWorkflowOrchestrationDependencyValidationException(Xeption innerException)
            : base(message: "Failed lab workflow orchestration dependency validation error occured. Please contact support.",
                  innerException)
        { }
    }
}
