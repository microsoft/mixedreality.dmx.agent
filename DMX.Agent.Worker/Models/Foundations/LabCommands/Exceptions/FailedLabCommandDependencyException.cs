// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabCommands.Exceptions
{
    public class FailedLabCommandDependencyException : Xeption
    {
        public FailedLabCommandDependencyException(Exception exception)
            : base(message: "Failed lab command event error occurred. Please contact support",
                  exception)
        { }
    }
}
