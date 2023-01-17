// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.Artifacts.Exceptions
{
    public class ArtifactValidationException : Xeption
    {
        public ArtifactValidationException(Xeption innerException)
            :base(message: "Artifact validation error has occurred. Please try again.",
                 innerException)
        { }
    }
}
