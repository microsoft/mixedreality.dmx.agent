// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.LabCommands.Exceptions
{
    public class NullLabCommandException : Xeption
    {
        public NullLabCommandException() :
            base(message: "Lab Command is null")
        { }
    }
}
