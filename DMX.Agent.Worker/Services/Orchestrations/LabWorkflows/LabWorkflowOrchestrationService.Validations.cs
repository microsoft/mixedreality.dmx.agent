// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabWorkflows;
using DMX.Agent.Worker.Models.Orchestrations.LabWorkflows;

namespace DMX.Agent.Worker.Services.Orchestrations.LabWorkflows
{
    public partial class LabWorkflowOrchestrationService
    {
        private void ValidateIfLabWorkflowIsNotNull(LabWorkflow workflow)
        {
            if (workflow is null)
            {
                throw new NullLabWorkflowException();
            }
        }
    }
}
