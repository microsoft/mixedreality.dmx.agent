// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions
{
    public class FailedLabArtifactServiceException : Xeption
    {
        public FailedLabArtifactServiceException(Exception innerException)
            : base(message: "Failed lab artifact error has occurred. Please contact support.",
                  innerException)
        { }
    }
}
