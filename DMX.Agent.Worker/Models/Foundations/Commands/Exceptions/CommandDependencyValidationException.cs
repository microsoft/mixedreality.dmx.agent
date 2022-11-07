// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.Commands.Exceptions
{
    public class CommandDependencyValidationException : Xeption
    {
        public CommandDependencyValidationException(Xeption innerException)
            : base(message: "Command dependency validation error occured. Please contact support.",
                  innerException)
        { }
    }
}
