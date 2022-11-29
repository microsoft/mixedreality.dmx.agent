// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Agent.Worker.Models.Orchestrations.LabWorkflows
{
    public class NullLabWorkflowException : Xeption
    {
        public NullLabWorkflowException()
            : base(message: "Lab workflow is null.")
        { }
    }
}
