// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Orchestrations.LabWorkflows.Exceptions
{
    public class FailedLabWorkflowOrchestrationDependencyException : Xeption
    {
        public FailedLabWorkflowOrchestrationDependencyException(Xeption innerException)
            : base(message: "Failed lab workflow orchestration dependency error occured. Please contact support.",
                  innerException)
        { }
    }
}
