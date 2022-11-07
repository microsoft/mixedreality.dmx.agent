// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.Commands.Exceptions
{
    public class EmptyCommandException : Xeption
    {
        public EmptyCommandException()
            : base(message: "Command is empty.")
        { }
    }
}
