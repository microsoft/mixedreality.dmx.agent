// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions
{
    public class NullLabWorkflowCommandException : Xeption
    {
        public NullLabWorkflowCommandException() :
            base(message: "Lab workflow command is null")
        { }
    }
}
