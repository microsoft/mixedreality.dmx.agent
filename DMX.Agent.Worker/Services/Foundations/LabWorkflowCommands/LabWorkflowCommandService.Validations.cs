// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowCommands
{
    public partial class LabWorkflowCommandService : ILabWorkflowCommandService
    {
        private static void ValidateLabWorkflowCommandIsNotNull(LabWorkflowCommand labWorkflowCommand)
        {
            if (labWorkflowCommand is null)
            {
                throw new NullLabWorkflowCommandException();
            }
        }
    }
}
