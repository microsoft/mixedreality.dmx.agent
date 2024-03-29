﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.Commands.Exceptions
{
    public class CommandValidationException : Xeption
    {
        public CommandValidationException(Xeption innerException)
            : base(message: "Command validation error occured. Please fix and try again.",
                  innerException)
        { }
    }
}
