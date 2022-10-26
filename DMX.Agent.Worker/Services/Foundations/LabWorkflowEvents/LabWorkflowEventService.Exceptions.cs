// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using DMX.Agent.Worker.Models.LabWorkflows.Exceptions;
using Microsoft.Azure.ServiceBus;
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
            catch (MessagingEntityNotFoundException messagingEntityNotFoundException)
            {
                var failedLabWorkflowDependencyException =
                    new FailedLabWorkflowDependencyException(messagingEntityNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedLabWorkflowDependencyException);
            }
            catch (MessagingEntityDisabledException messagingEntityDisabledException)
            {
                var failedLabWorkflowDependencyException =
                    new FailedLabWorkflowDependencyException(messagingEntityDisabledException);

                throw CreateAndLogCriticalDependencyException(failedLabWorkflowDependencyException);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                var failedLabWorkflowDependencyException =
                    new FailedLabWorkflowDependencyException(unauthorizedAccessException);

                throw CreateAndLogCriticalDependencyException(failedLabWorkflowDependencyException);
            }
        }

        private LabWorkflowDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var labWorkflowDependencyException =
                new LabWorkflowDependencyException(exception);

            this.loggingBroker.LogCritical(labWorkflowDependencyException);

            return labWorkflowDependencyException;
        }

        private LabWorkflowValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labWorkflowValidationException =
                new LabWorkflowValidationException(exception); 

            this.loggingBroker.LogError(labWorkflowValidationException);

            return labWorkflowValidationException;
        }
    }
}