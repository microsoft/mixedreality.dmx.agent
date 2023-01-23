// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions
{
    public class LabArtifactServiceException : Xeption
    {
        public LabArtifactServiceException(Xeption innerException)
            : base(message: "Lab artifact service error has occurred. Please contact support.",
                  innerException)
        { }
    }
}
