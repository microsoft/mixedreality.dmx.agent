// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabCommands.Exceptions
{
    public class LabCommandDependencyException : Xeption
    {
        public LabCommandDependencyException(Xeption innerException) :
            base(message: "Lab command dependency error occurred. Please contact support.",
                innerException)
        { }
    }
}
