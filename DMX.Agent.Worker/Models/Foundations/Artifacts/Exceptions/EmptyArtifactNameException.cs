// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.Artifacts.Exceptions
{
    public class EmptyArtifactNameException : Xeption
    {
        public EmptyArtifactNameException()
            :base(message: "Artifact name is empty. Please contact support")
        { }
    }
}
