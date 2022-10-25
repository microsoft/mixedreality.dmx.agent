// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using DMX.Agent.Worker.Models.LabCommands.Exceptions;
using Microsoft.Azure.ServiceBus;
using Xeptions;
using AzureMessagingCommunicationException = Microsoft.ServiceBus.Messaging.MessagingCommunicationException;

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
            catch (NullLabCommandHandlerException nullLabCommandException)
            {
                throw CreateAndLogValidationException(nullLabCommandException);
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
            catch (InvalidOperationException InvalidOperationException)
            {
                var failedLabCommandDependencyException = new FailedLabCommandDependencyException(InvalidOperationException);

                throw CreateAndLogDependencyException(failedLabCommandDependencyException);
            }
            catch (AzureMessagingCommunicationException AzureMessagingCommunicationException)
            {
                var failedLabCommandDependencyException = new FailedLabCommandDependencyException(AzureMessagingCommunicationException);

                throw CreateAndLogDependencyException(failedLabCommandDependencyException);
            }
            catch (ServerBusyException ServerBusyException)
            {
                var failedLabCommandDependencyException = new FailedLabCommandDependencyException(ServerBusyException);

                throw CreateAndLogDependencyException(failedLabCommandDependencyException);
            }
            catch (Exception exception)
            {
                var failedLabCommandServiceException = new FailedLabCommandServiceException(exception);

                throw CreateAndLogServiceException(failedLabCommandServiceException);
            }
        }

        private LabCommandValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labCommandValidationException = new LabCommandValidationException(exception);
            this.loggingBroker.LogError(labCommandValidationException);

            return labCommandValidationException;
        }

        private LabCommandDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var labCommandDependencyException = new LabCommandDependencyException(exception);
            this.loggingBroker.LogError(labCommandDependencyException);

            return labCommandDependencyException;
        }

        private LabCommandDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var labCommandDependencyException = new LabCommandDependencyException(exception);
            this.loggingBroker.LogCritical(labCommandDependencyException);

            return labCommandDependencyException;
        }

        private LabCommandServiceException CreateAndLogServiceException(Xeption exception)
        {
            var labCommandServiceException = new LabCommandServiceException(exception);
            this.loggingBroker.LogError(labCommandServiceException);

            return labCommandServiceException;
        }
    }
}
