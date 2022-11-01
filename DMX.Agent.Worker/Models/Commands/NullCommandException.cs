// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Commands
{
    public class NullCommandException : Xeption
    {
        public NullCommandException()
            : base(message: "Command is null.")
        { }
    }
}
