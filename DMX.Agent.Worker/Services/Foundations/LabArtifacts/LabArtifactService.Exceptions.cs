// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Azure;
using DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Net;
using System.Threading.Tasks;
using Xeptions;

namespace DMX.Agent.Worker.Services.Foundations.Artifacts
{
    public partial class LabArtifactService
    {
        private delegate ValueTask<Response> ReturningResponseFunction();

        private async ValueTask<Response> TryCatch(ReturningResponseFunction returningResponseFunction)
        {
            try
            {
                return await returningResponseFunction();
            }
            catch (EmptyLabArtifactNameException emptyArtifactNameException)
            {
                throw CreateAndLogValidationException(emptyArtifactNameException);
            }
            catch (RequestFailedException requestFailedException)
                when (requestFailedException.Status
                    is (int)HttpStatusCode.Unauthorized
                    or (int)HttpStatusCode.Forbidden)
            {
                var failedLabArtifactDependencyException =
                    new FailedLabArtifactDependencyException(
                        requestFailedException);

                throw CreateAndLogCriticalDependencyException(
                    failedLabArtifactDependencyException);
            }
            catch (RequestFailedException requestFailedException)
                when (requestFailedException.Status
                    is (int)HttpStatusCode.NotFound)
            {
                var notFoundLabArtifactException =
                    new NotFoundLabArtifactException(
                        requestFailedException);

                throw CreateAndLogDependencyValidationException(notFoundLabArtifactException);
            }
            catch (RequestFailedException requestFailedException)
            {
                var failedArtifactDependencyException =
                    new FailedLabArtifactDependencyException(
                        requestFailedException);

                throw CreateAndLogDependencyException(failedArtifactDependencyException);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                var labArtifactFilePathUnauthorizedException =
                    new LabArtifactFilePathUnauthorizedException(
                        unauthorizedAccessException);
                
                throw CreateAndLogDependencyValidationException(labArtifactFilePathUnauthorizedException);
            }
            catch (Exception exception)
            {
                var failedLabArtifactServiceException =
                    new FailedLabArtifactServiceException(exception);

                throw CreateAndLogServiceException(failedLabArtifactServiceException);
            }
        }

        private Exception CreateAndLogServiceException(FailedLabArtifactServiceException failedLabArtifactServiceException)
        {
            var labArtifactServiceException =
                new LabArtifactServiceException(failedLabArtifactServiceException);

            this.loggingBroker.LogError(labArtifactServiceException);

            return labArtifactServiceException;
        }

        private LabArtifactDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var labArtifactDependencyValidationException =
                new LabArtifactDependencyValidationException(exception);

            this.loggingBroker.LogError(labArtifactDependencyValidationException);

            return labArtifactDependencyValidationException;
        }

        private LabArtifactValidationException CreateAndLogValidationException(Xeption exception)
        {
            var artifactValidationException =
                new LabArtifactValidationException(exception);

            this.loggingBroker.LogError(artifactValidationException);

            return artifactValidationException;
        }

        private LabArtifactDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var artifactDependencyException = new LabArtifactDependencyException(exception);
            this.loggingBroker.LogCritical(artifactDependencyException);

            return artifactDependencyException;
        }

        private LabArtifactDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var artifactDependencyException = new LabArtifactDependencyException(exception);
            this.loggingBroker.LogError(artifactDependencyException);

            return artifactDependencyException;
        }
    }
}
