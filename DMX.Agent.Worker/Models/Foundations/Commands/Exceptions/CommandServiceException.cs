// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.Commands.Exceptions
{
    public class CommandServiceException : Xeption
    {
        public CommandServiceException(Xeption exception)
            : base(message: "Command service error occurred. Please contact support.",
                 exception)
        { }
    }
}
