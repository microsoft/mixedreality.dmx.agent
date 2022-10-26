// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions
{
    public class InvalidLabWorkflowCommandException : Xeption
    {
        public InvalidLabWorkflowCommandException()
            : base(message: "Invalid lab workflow command error occurred. Please fix the errors and try again.")
        { }

        public InvalidLabWorkflowCommandException(Exception innerException, IDictionary data)
            : base(message: "Invalid lab workflow command error occurred. Please fix the errors and try again.",
                 innerException,
                 data)
        { }
    }
}
