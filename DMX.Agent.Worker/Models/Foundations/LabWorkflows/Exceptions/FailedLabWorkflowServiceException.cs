// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflows.Exceptions
{
    public class FailedLabWorkflowServiceException : Xeption
    {
        public FailedLabWorkflowServiceException(Exception exception)
            : base(message: "Failed lab workflow exception occurred. Please contact support.",
                 exception)
        { }
    }
}
