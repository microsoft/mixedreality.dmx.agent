// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.Artifacts.Exceptions
{
    public class FailedArtifactDependencyException : Xeption
    {
        public FailedArtifactDependencyException(Exception innerException)
            :base (message:"Failed artifact dependency error has occurred. Please contact support.",
                 innerException)
        { }
    }
}
