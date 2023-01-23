// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions
{
    public class LabArtifactValidationException : Xeption
    {
        public LabArtifactValidationException(Xeption innerException)
            : base(message: "Artifact validation error has occurred. Please try again.",
                 innerException)
        { }
    }
}
