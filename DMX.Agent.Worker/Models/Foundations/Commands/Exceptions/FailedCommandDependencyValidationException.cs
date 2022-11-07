// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.Commands.Exceptions
{
    public class FailedCommandDependencyValidationException : Xeption
    {
        public FailedCommandDependencyValidationException(Exception innerException)
            : base(message: "Failed command dependency validation error occured. Please contact support.",
                  innerException)
        { }
    }
}
