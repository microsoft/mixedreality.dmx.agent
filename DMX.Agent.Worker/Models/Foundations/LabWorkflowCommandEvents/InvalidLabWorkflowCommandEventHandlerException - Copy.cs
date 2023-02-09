// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflowCommandEvents
{
    public class InvalidLabWorkflowCommandEventHandlerException : Xeption
    {
        public InvalidLabWorkflowCommandEventHandlerException(Exception innerException)
            : base(message: "Invalid Lab workflow command event handler error occurred, fix errors and try again.")
        { }
    }
}
