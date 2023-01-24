// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions
{
    public class LabArtifactDependencyValidationException : Xeption
    {
        public LabArtifactDependencyValidationException(Xeption innerException)
            : base(message: "Lab artifact dependency validation error has occurred. Please try again.",
                  innerException)
        { }
    }
}
