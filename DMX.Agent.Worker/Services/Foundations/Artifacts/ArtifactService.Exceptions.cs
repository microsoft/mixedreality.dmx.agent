﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Azure;
using DMX.Agent.Worker.Models.Foundations.Artifacts.Exceptions;
using Microsoft.ServiceBus.Messaging;
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
            catch(RequestFailedException requestFailedException)
            {
                var failedArtifactDependencyException =
                    new FailedArtifactDependencyException(requestFailedException);

                throw CreateAndLogDependencyException(failedArtifactDependencyException);
            }
        }

        private ArtifactValidationException CreateAndLogValidationException(Xeption exception)
        {
            ArtifactValidationException artifactValidationException =
                new ArtifactValidationException(exception);

            this.loggingBroker.LogError(artifactValidationException);

            return artifactValidationException;
        }

        private ArtifactDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var artifactDependencyException = new ArtifactDependencyException(exception);
            this.loggingBroker.LogError(artifactDependencyException);

            return artifactDependencyException;
        }
    }
}
