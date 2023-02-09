// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflowCommandEvents
{
    public class LabWorkflowCommandEventDependencyValidationException : Xeption
    {
        public LabWorkflowCommandEventDependencyValidationException(Xeption innerException)
            : base(message: "Lab workflow command event dependency validation error occurred. please try again",
                  innerException)
        { }
    }
}
