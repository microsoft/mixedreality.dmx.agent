// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions
{
    public class LabWorkflowCommandServiceException : Xeption
    {
        public LabWorkflowCommandServiceException(Xeption innerException)
            : base(message: "Lab workflow command service error occurred, contact support",
                  innerException)
        { }
    }
}
