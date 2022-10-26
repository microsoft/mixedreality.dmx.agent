// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.LabWorkflows.Exceptions
{
    public class LabWorkflowServiceException : Xeption
    {
        public LabWorkflowServiceException(Xeption exception)
            :base(message: "Lab workflow service error occurred. Please contact support.",
                 exception)
        { }
    }
}
