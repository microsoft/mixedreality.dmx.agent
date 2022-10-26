﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions;
using RESTFulSense.Exceptions;
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
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedLabWorkflowCommandDependencyException =
                    new FailedLabWorkflowCommandDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedLabWorkflowCommandDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedLabWorkflowCommandDependencyException =
                    new FailedLabWorkflowCommandDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedLabWorkflowCommandDependencyException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var failedLabWorkflowCommandDependencyException =
                    new FailedLabWorkflowCommandDependencyException(httpResponseForbiddenException);

                throw CreateAndLogCriticalDependencyException(failedLabWorkflowCommandDependencyException);
            }
        }

        private LabWorkflowCommandValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labWorkflowCommandValidationException =
                new LabWorkflowCommandValidationException(exception);

            this.loggingBroker.LogError(labWorkflowCommandValidationException);

            return labWorkflowCommandValidationException;
        }

        private LabWorkflowCommandDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var labWorkflowCommandDependencyException =
                new LabWorkflowCommandDependencyException(exception);

            this.loggingBroker.LogCritical(labWorkflowCommandDependencyException);

            return labWorkflowCommandDependencyException;
        }
    }
}
