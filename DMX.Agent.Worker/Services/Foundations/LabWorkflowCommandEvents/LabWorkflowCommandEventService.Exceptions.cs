// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommandEvents;
using Xeptions;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowCommandEvents
{
    public partial class LabWorkflowCommandEventService
    {
        private delegate void ReturningNothingFunction();

        private void TryCatch(ReturningNothingFunction returningNothingFunction)
        {
            try
            {
                returningNothingFunction();
            }
            catch (NullLabWorkflowCommandEventHandlerException nullLabWorkflowCommandEventHandlerException)
            {
                throw CreateAndLogValidationException(nullLabWorkflowCommandEventHandlerException);
            }
        }

        private LabWorkflowCommandEventValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labWorkflowCommandEventValidationException =
                new LabWorkflowCommandEventValidationException(exception);

            this.loggingBroker.LogError(labWorkflowCommandEventValidationException);

            return labWorkflowCommandEventValidationException;
        }
    }
}
