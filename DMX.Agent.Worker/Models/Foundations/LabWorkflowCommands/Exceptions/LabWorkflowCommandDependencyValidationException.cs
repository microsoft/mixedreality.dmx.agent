﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions
{
    public class LabWorkflowCommandDependencyValidationException : Xeption
    {
        public LabWorkflowCommandDependencyValidationException(Xeption innerException)
            : base(message: "Lab workflow command dependency validation error occurred. Please fix and try again.",
                  innerException)
        { }
    }
}
