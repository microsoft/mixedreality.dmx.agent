// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions;
using Xeptions;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowCommands
{
    public partial class LabWorkflowCommandService : ILabWorkflowCommandService
    {
        private delegate ValueTask<LabWorkflowCommand> ReturningLabWorkflowCommandFunction();

        private async ValueTask<LabWorkflowCommand> TryCatch(ReturningLabWorkflowCommandFunction returningLabWorkflowCommandFunction)
        {
            try
            {
                return await returningLabWorkflowCommandFunction();
            }
            catch (NullLabWorkflowCommandException nullLabWorkflowCommandException)
            {
                throw CreateAndLogValidationException(nullLabWorkflowCommandException);
            }
            catch (InvalidLabWorkflowCommandException invalidLabWorkflowCommandException)
            {
                throw CreateAndLogValidationException(invalidLabWorkflowCommandException);
            }
        }

        private LabWorkflowCommandValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labWorkflowCommandValidationException =
                new LabWorkflowCommandValidationException(exception);

            this.loggingBroker.LogError(labWorkflowCommandValidationException);

            return labWorkflowCommandValidationException;
        }
    }
}
