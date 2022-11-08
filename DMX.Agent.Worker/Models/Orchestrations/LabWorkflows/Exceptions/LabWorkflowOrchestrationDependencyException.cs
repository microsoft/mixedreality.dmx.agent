// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Orchestrations.LabWorkflows.Exceptions
{
    public class LabWorkflowOrchestrationDependencyException : Xeption
    {
        public LabWorkflowOrchestrationDependencyException(Xeption innerException) 
            : base(message: "Lab workflow orchestration dependency error occured. Please contact support.",
                  innerException)
        { }
    }
}
