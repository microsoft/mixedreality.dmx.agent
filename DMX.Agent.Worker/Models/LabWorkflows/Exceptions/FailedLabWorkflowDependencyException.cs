// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.LabWorkflows.Exceptions
{
    public class FailedLabWorkflowDependencyException : Xeption
    {
        public FailedLabWorkflowDependencyException(Exception exception)
            :base (message: "Failed lab workflow dependency error occurred. Please contact support.",
                 exception)
        { }
    }
}
