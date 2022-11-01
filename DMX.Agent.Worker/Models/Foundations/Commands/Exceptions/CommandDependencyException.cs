// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.Commands.Exceptions
{
    public class CommandDependencyException : Xeption
    {
        public CommandDependencyException(Xeption exception)
            : base(message: "Command dependency error occurred. Please cntact support",
                 exception)
        { }
    }
}
