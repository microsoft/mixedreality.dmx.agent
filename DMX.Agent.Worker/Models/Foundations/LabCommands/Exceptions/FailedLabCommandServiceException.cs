// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabCommands.Exceptions
{
    public class FailedLabCommandServiceException : Xeption
    {
        public FailedLabCommandServiceException(Exception innerException)
            : base(message: "Failed lab command service error ocurred, contact support.",
                  innerException)
        { }
    }
}
