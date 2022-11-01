// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.Commands
{
    public class FailedCommandDependencyException : Xeption
    {
        public FailedCommandDependencyException(Exception exception)
            : base(message: "Failed command dependency error occurred. Please contact support",
                 exception)
        { }
    }
}
