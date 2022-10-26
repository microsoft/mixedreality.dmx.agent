// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions
{
    public class InvalidLabWorkflowCommandException : Xeption
    {
        public InvalidLabWorkflowCommandException()
            : base(message: "Invalid lab workflow command error occurred. Please fix the errors and try again.")
        { }
    }
}
