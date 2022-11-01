// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.LabWorkflows.Exceptions
{
    public class LabWorkflowDependencyException : Xeption
    {
        public LabWorkflowDependencyException(Xeption exception)
            : base(message: "Lab workflow dependency error occurred. Please contact support.",
                 exception)
        { }
    }
}
