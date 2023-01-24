// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions
{
    public class EmptyLabArtifactNameException : Xeption
    {
        public EmptyLabArtifactNameException()
            : base(message: "Artifact name or file path is empty. Please contact support")
        { }
    }
}
