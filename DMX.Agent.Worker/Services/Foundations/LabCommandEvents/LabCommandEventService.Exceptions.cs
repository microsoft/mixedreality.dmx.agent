// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using DMX.Agent.Worker.Models.LabCommands.Exceptions;
using Microsoft.Azure.ServiceBus;
using Xeptions;

namespace DMX.Agent.Worker.Services.Foundations.LabCommandEvents
{
    public partial class LabCommandEventService
    {
        private delegate void ReturningNothingFunction();

        private void TryCatch(ReturningNothingFunction returningNothingFunction)
        {
            try
            {
                returningNothingFunction();
            }
            catch (MessagingEntityNotFoundException messagingEntityNotFoundException)
            {
                var failedLabCommandDependencyException =
                    new FailedLabCommandDependencyException(messagingEntityNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedLabCommandDependencyException);
            }
            catch (MessagingEntityDisabledException messagingEntityDisabledException)
            {
                var failedLabCommandDependencyException =
                    new FailedLabCommandDependencyException(messagingEntityDisabledException);

                throw CreateAndLogCriticalDependencyException(failedLabCommandDependencyException);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                var failedLabCommandDependencyException =
                    new FailedLabCommandDependencyException(unauthorizedAccessException);

                throw CreateAndLogCriticalDependencyException(failedLabCommandDependencyException);
            }
            catch (Exception exception)
            {
                var failedLabCommandServiceException = new FailedLabCommandServiceException(exception);

                throw CreateAndLogServiceException(failedLabCommandServiceException);
            }
        }

        private Exception CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var labCommandDependencyException = new LabCommandDependencyException(exception);
            this.loggingBroker.LogCritical(labCommandDependencyException);

            return labCommandDependencyException;
        }

        private Xeption CreateAndLogServiceException(Xeption exception)
        {
            var labCommandServiceException = new LabCommandServiceException(exception);
            this.loggingBroker.LogError(labCommandServiceException);

            return labCommandServiceException;
        }
    }
}
