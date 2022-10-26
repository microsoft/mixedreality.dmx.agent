// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions
{
    public class LabWorkflowCommandValidationException : Xeption
    {
        public LabWorkflowCommandValidationException(Xeption innerException)
            : base(message: "Lab workflow command is invalid, please fix errors and try again",
                   innerException)
        { }
    }
}
