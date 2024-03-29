﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabCommands.Exceptions
{
    public class NullLabCommandHandlerException : Xeption
    {
        public NullLabCommandHandlerException() :
            base(message: "Lab Command handler is null")
        { }
    }
}
