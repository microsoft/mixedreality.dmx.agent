// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.Commands.Exceptions;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows.Exceptions;
using DMX.Agent.Worker.Models.Orchestrations.LabWorkflows;
using DMX.Agent.Worker.Models.Orchestrations.LabWorkflows.Exceptions;
using Xeptions;

namespace DMX.Agent.Worker.Services.Orchestrations.LabWorkflows
{
    public partial class LabWorkflowOrchestrationService
    {
        private delegate ValueTask ReturningNothingFunctionAsync();
        private delegate void ReturningNothingFunction();

        private async ValueTask TryCatch(ReturningNothingFunctionAsync returningNothingFunctionAsync)
        {
            try
            {
                await returningNothingFunctionAsync();
            }
            catch (NullLabWorkflowException nullLabWorkflowException)
            {
                var failedLabWorkflowValidationException =
                    new FailedLabWorkflowOrchestrationValidationException(
                        nullLabWorkflowException);

                throw CreateAndLogOrchestrationValidationException(failedLabWorkflowValidationException);
            }
            catch (LabWorkflowCommandDependencyValidationException labWorkflowCommandDependencyValidationException)
            {
                var failedLabWorkflowOrchestrationDependencyValidationException =
                    new FailedLabWorkflowOrchestrationDependencyValidationException(
                        labWorkflowCommandDependencyValidationException);

                throw CreateAndLogOrchestrationDependencyValidationException(
                    failedLabWorkflowOrchestrationDependencyValidationException);
            }
            catch (LabWorkflowCommandValidationException labWorkflowCommandValidationException)
            {
                var failedLabWorkflowOrchestrationDependencyValidationException =
                    new FailedLabWorkflowOrchestrationDependencyValidationException(
                        labWorkflowCommandValidationException);

                throw CreateAndLogOrchestrationDependencyValidationException(
                    failedLabWorkflowOrchestrationDependencyValidationException);
            }
            catch (CommandDependencyValidationException commandDependencyValidationException)
            {
                var failedLabWorkflowOrchestrationDependencyValidationException =
                    new FailedLabWorkflowOrchestrationDependencyValidationException(
                        commandDependencyValidationException);

                throw CreateAndLogOrchestrationDependencyValidationException(
                    failedLabWorkflowOrchestrationDependencyValidationException);
            }
            catch (CommandValidationException commandValidationException)
            {
                var failedLabWorkflowOrchestrationDependencyValidationException =
                    new FailedLabWorkflowOrchestrationDependencyValidationException(
                        commandValidationException);

                throw CreateAndLogOrchestrationDependencyValidationException(
                    failedLabWorkflowOrchestrationDependencyValidationException);
            }
            catch (LabWorkflowCommandDependencyException labWorkflowCommandDependencyException)
            {
                var failedLabWorkflowOrchestrationDependencyException =
                    new FailedLabWorkflowOrchestrationDependencyException(
                        labWorkflowCommandDependencyException);

                throw CreateAndLogOrchestrationDependencyException(
                    failedLabWorkflowOrchestrationDependencyException);
            }

            catch (LabWorkflowCommandServiceException labWorkflowCommandServiceException)
            {
                var failedLabWorkflowOrchestrationDependencyException =
                    new FailedLabWorkflowOrchestrationDependencyException(
                        labWorkflowCommandServiceException);

                throw CreateAndLogOrchestrationDependencyException(
                    failedLabWorkflowOrchestrationDependencyException);
            }

            catch (CommandDependencyException commandDependencyException)
            {
                var failedLabWorkflowOrchestrationDependencyException =
                    new FailedLabWorkflowOrchestrationDependencyException(
                        commandDependencyException);

                throw CreateAndLogOrchestrationDependencyException(
                    failedLabWorkflowOrchestrationDependencyException);
            }
            catch (CommandServiceException commandServiceException)
            {
                var failedLabWorkflowOrchestrationDependencyException =
                    new FailedLabWorkflowOrchestrationDependencyException(
                        commandServiceException);

                throw CreateAndLogOrchestrationDependencyException(
                    failedLabWorkflowOrchestrationDependencyException);
            }
            catch (Exception exception)
            {
                var failedLabWorkflowOrchestrationServiceException =
                    new FailedLabWorkflowOrchestrationServiceException(
                        exception);

                throw CreateAndLogOrchestrationServiceException(
                    failedLabWorkflowOrchestrationServiceException);
            }
        }

        private void TryCatch(ReturningNothingFunction returningNothingFunction)
        {
            try
            {
                returningNothingFunction();
            }
            catch(LabWorkflowValidationException labWorkflowValidationException)
            {
                throw CreateAndLogOrchestrationDependencyValidationException(labWorkflowValidationException);
            }
        }

        private LabWorkflowOrchestrationValidationException CreateAndLogOrchestrationValidationException(Xeption exception)
        {
            var labWorkflowOrchestrationValidationException =
                new LabWorkflowOrchestrationValidationException(exception);

            this.loggingBroker.LogError(labWorkflowOrchestrationValidationException);

            return labWorkflowOrchestrationValidationException;
        }

        private LabWorkflowOrchestrationDependencyValidationException CreateAndLogOrchestrationDependencyValidationException(Xeption exception)
        {
            var labWorkflowOrchestrationDependencyValidationException =
                new LabWorkflowOrchestrationDependencyValidationException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(labWorkflowOrchestrationDependencyValidationException);

            return labWorkflowOrchestrationDependencyValidationException;
        }

        private LabWorkflowOrchestrationDependencyException CreateAndLogOrchestrationDependencyException(Xeption exception)
        {
            var labWorkflowOrchestrationDependencyException =
                new LabWorkflowOrchestrationDependencyException(exception);

            this.loggingBroker.LogError(labWorkflowOrchestrationDependencyException);

            return labWorkflowOrchestrationDependencyException;
        }

        private LabWorkflowOrchestrationServiceException CreateAndLogOrchestrationServiceException(Xeption exception)
        {
            var labWorkflowOrchestrationServiceException =
                new LabWorkflowOrchestrationServiceException(exception);

            this.loggingBroker.LogError(labWorkflowOrchestrationServiceException);

            return labWorkflowOrchestrationServiceException;
        }
    }
}
