// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions
{
    public class NotFoundLabArtifactException : Xeption
    {
        public NotFoundLabArtifactException(Exception innerException)
            : base(message: "Lab artifact not found.",
                  innerException)
        { }
    }
}
