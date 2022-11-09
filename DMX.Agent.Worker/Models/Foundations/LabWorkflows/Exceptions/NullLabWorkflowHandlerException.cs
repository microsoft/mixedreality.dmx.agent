// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflows.Exceptions
{
    public class NullLabWorkflowHandlerException : Xeption
    {
        public NullLabWorkflowHandlerException()
            : base(message: "Lab workflow handler is null.")
        { }
    }
}
