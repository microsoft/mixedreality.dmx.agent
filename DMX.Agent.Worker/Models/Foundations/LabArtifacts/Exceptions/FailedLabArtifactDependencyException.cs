// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions
{
    public class FailedLabArtifactDependencyException : Xeption
    {
        public FailedLabArtifactDependencyException(Exception innerException)
            : base(message: "Failed artifact dependency error has occurred. Please contact support.",
                 innerException)
        { }
    }
}
