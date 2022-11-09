// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflows.Exceptions
{
    public class LabWorkflowValidationException : Xeption
    {
        public LabWorkflowValidationException(Xeption innerException)
            : base(message: "Lab workflow validation error occured. Please contact support.",
                  innerException)
        { }
    }
}
