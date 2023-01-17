// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.Artifacts.Exceptions
{
    public class ArtifactDependencyException : Xeption
    {
        public ArtifactDependencyException(Xeption innerException)
            :base(message: "Artifact Dependency error occurred. Please contact support",
                 innerException)
        { }
    }
}
