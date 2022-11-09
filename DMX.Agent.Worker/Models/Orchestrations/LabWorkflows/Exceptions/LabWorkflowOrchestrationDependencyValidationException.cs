﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Orchestrations.LabWorkflows.Exceptions
{
    public class LabWorkflowOrchestrationDependencyValidationException : Xeption
    {
        public LabWorkflowOrchestrationDependencyValidationException(Xeption innerException) 
            : base(message: "Lab workflow orchestration dependency validation error occured. Please contact support.",
                  innerException)
        { }
    }
}
