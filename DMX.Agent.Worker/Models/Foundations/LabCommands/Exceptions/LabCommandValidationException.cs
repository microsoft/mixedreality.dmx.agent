// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabCommands.Exceptions
{
    public class LabCommandValidationException : Xeption
    {
        public LabCommandValidationException(Xeption exception)
            : base(message: "Lab Command validation error occurred. Please contact support",
                 exception)
        { }
    }
}
