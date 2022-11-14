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
                throw CreateAndLogOrchestrationDependencyValidationException(
                    labWorkflowCommandDependencyValidationException);
            }
            catch (LabWorkflowCommandValidationException labWorkflowCommandValidationException)
            {
                throw CreateAndLogOrchestrationDependencyValidationException(
                    labWorkflowCommandValidationException);
            }
            catch (CommandDependencyValidationException commandDependencyValidationException)
            {
                throw CreateAndLogOrchestrationDependencyValidationException(
                    commandDependencyValidationException);
            }
            catch (CommandValidationException commandValidationException)
            {
                throw CreateAndLogOrchestrationDependencyValidationException(
                    commandValidationException);
            }
            catch (LabWorkflowCommandDependencyException labWorkflowCommandDependencyException)
            {
                throw CreateAndLogOrchestrationDependencyException(
                    labWorkflowCommandDependencyException);
            }

            catch (LabWorkflowCommandServiceException labWorkflowCommandServiceException)
            {
                throw CreateAndLogOrchestrationDependencyException(
                    labWorkflowCommandServiceException);
            }
            catch (CommandDependencyException commandDependencyException)
            {
                throw CreateAndLogOrchestrationDependencyException(
                    commandDependencyException);
            }
            catch (CommandServiceException commandServiceException)
            {
                throw CreateAndLogOrchestrationDependencyException(
                    commandServiceException);
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
            catch (LabWorkflowCommandDependencyException labWorkflowCommandDependencyException)
            {
                throw CreateAndLogOrchestrationDependencyException(
                    labWorkflowCommandDependencyException);
            }
            catch (LabWorkflowCommandServiceException labWorkflowCommandServiceException)
            {
                throw CreateAndLogOrchestrationDependencyException(
                    labWorkflowCommandServiceException);
            }
            catch (LabWorkflowDependencyException labWorkflowDependencyException)
            {
                throw CreateAndLogOrchestrationDependencyException(
                    labWorkflowDependencyException);
            }
            catch (LabWorkflowServiceException labWorkflowServiceException)
            {
                throw CreateAndLogOrchestrationDependencyException(
                    labWorkflowServiceException);
            }
            catch (CommandDependencyException commandDependencyException)
            {
                throw CreateAndLogOrchestrationDependencyException(
                    commandDependencyException);
            }
            catch (CommandServiceException commandServiceException)
            {
                throw CreateAndLogOrchestrationDependencyException(
                    commandServiceException);
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
                new LabWorkflowOrchestrationDependencyException(exception.InnerException as Xeption);

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
