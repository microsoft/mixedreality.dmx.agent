// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflowCommandEvents
{
    public class LabWorkflowCommandEventValidationException : Xeption
    {
        public LabWorkflowCommandEventValidationException(Xeption innerException)
            : base(message: "Lab workflow command event validation error occurred. Please fix and try again.",
                  innerException)
        { }
    }
}
