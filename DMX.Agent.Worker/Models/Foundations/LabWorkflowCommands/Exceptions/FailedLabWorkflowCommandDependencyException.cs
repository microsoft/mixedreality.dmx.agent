// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions
{
    public class FailedLabWorkflowCommandDependencyException : Xeption
    {
        public FailedLabWorkflowCommandDependencyException(Exception innerException)
            : base(message: "Failed lab workflow command dependency error occurred, contact support",
                  innerException)
        { }
    }
}
