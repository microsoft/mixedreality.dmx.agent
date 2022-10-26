// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using DMX.Agent.Worker.Models.LabWorkflows.Exceptions;
using Xeptions;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowEvents
{
    public partial class LabWorkflowEventService
    {
        private delegate void ReturningNothingFunction();

        private void TryCatch(ReturningNothingFunction returningNothingFunction)
        {
            try
            {
                returningNothingFunction();
            }
            catch (NullLabWorkflowHandlerException nullLabWorkflowHandlerException)
            {
                throw CreateAndLogValidationException(nullLabWorkflowHandlerException);
            }
        }

        private LabWorkflowValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labWorkflowValidationException = new LabWorkflowValidationException(exception);
            this.loggingBroker.LogError(labWorkflowValidationException);

            return labWorkflowValidationException;
        }
    }
}