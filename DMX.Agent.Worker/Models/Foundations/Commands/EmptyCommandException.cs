// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Commands
{
    public class EmptyCommandException : Xeption
    {
        public EmptyCommandException()
            : base(message: "Command is empty.")
        { }
    }
}
