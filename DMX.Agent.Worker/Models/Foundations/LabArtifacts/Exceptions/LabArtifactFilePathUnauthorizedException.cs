// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions
{
    public class LabArtifactFilePathUnauthorizedException : Xeption
    {
        public LabArtifactFilePathUnauthorizedException(Exception innerException)
            : base(message: "Lab artifact file path authorization not valid. Please contact support.",
                  innerException)
        { }
    }
}
