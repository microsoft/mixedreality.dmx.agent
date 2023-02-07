// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflowCommandEvents
{
    public class NullLabWorkflowCommandEventHandlerException : Xeption
    {
        public NullLabWorkflowCommandEventHandlerException()
            : base(message: "Lab workflow command event handler is null.")
        { }
    }
}
