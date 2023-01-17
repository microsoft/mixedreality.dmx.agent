// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.Artifacts.Exceptions
{
    public class EmptyArtifactFilePathException : Xeption
    {
        public EmptyArtifactFilePathException()
            : base(message: "Artifact file path is empty. Please try again.")
        { }
    }
}
