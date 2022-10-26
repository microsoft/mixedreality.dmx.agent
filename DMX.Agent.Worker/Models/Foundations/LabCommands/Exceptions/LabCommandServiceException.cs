// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabCommands.Exceptions
{
    public class LabCommandServiceException : Xeption
    {
        public LabCommandServiceException(Xeption innerException)
            : base(message: "Lab command service error failed, contact support.",
                  innerException)
        { }
    }
}
