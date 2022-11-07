// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.Commands.Exceptions
{
    public class FailedCommandServiceException : Xeption
    {
        public FailedCommandServiceException(Exception exception)
            :base(message:"Failed command service error occurred. Please contact support",
                 exception)
        { }
    }
}
