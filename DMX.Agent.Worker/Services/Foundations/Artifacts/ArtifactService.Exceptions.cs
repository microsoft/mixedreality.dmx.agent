﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Azure;
using DMX.Agent.Worker.Models.Foundations.Artifacts.Exceptions;
using DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Net;
using System.Threading.Tasks;
using Xeptions;

namespace DMX.Agent.Worker.Services.Foundations.Artifacts
{
    public partial class ArtifactService
    {
        private delegate ValueTask<Response> ReturningResponseFunction();

        private async ValueTask<Response> TryCatch(ReturningResponseFunction returningResponseFunction)
        {
            try
            {
                return await returningResponseFunction();
            }
            catch (EmptyArtifactNameException emptyArtifactNameException)
            {
                throw CreateAndLogValidationException(emptyArtifactNameException);
            }
            catch (RequestFailedException requestFailedException)
                when (requestFailedException.Status
                    is (int)HttpStatusCode.Unauthorized
                    or (int)HttpStatusCode.Forbidden)
            {
                var failedLabArtifactDependencyException =
                    new FailedArtifactDependencyException(
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
                    new FailedArtifactDependencyException(requestFailedException);

                throw CreateAndLogDependencyException(failedArtifactDependencyException);
            }
        }

        private LabArtifactDependencyValidationException CreateAndLogDependencyValidationException(NotFoundLabArtifactException notFoundLabArtifactException)
        {
            var artifactValidationException =
                new LabArtifactDependencyValidationException(notFoundLabArtifactException);

            this.loggingBroker.LogError(artifactValidationException);

            return artifactValidationException;
        }

        private ArtifactValidationException CreateAndLogValidationException(Xeption exception)
        {
            var artifactValidationException =
                new ArtifactValidationException(exception);

            this.loggingBroker.LogError(artifactValidationException);

            return artifactValidationException;
        }

        private ArtifactDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var artifactDependencyException = new ArtifactDependencyException(exception);
            this.loggingBroker.LogCritical(artifactDependencyException);

            return artifactDependencyException;
        }

        private ArtifactDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var artifactDependencyException = new ArtifactDependencyException(exception);
            this.loggingBroker.LogError(artifactDependencyException);

            return artifactDependencyException;
        }
    }
}
