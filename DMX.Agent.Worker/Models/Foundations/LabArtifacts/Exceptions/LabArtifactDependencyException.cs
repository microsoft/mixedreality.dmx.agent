// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions
{
    public class LabArtifactDependencyException : Xeption
    {
        public LabArtifactDependencyException(Xeption innerException)
            : base(message: "Artifact Dependency error occurred. Please contact support",
                 innerException)
        { }
    }
}
