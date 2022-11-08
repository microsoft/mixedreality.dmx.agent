// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.Commands.Exceptions;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions;
using DMX.Agent.Worker.Models.Orchestrations.LabWorkflows;
using DMX.Agent.Worker.Models.Orchestrations.LabWorkflows.Exceptions;
using Xeptions;

namespace DMX.Agent.Worker.Services.Orchestrations.LabWorkflows
{
    public partial class LabWorkflowOrchestrationService
    {
        private delegate ValueTask ReturningNothingFunction();

        private async ValueTask TryCatch(ReturningNothingFunction returningNothingFunction)
        {
            try
            {
                await returningNothingFunction();
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
                new LabWorkflowOrchestrationDependencyValidationException(exception);

            this.loggingBroker.LogError(labWorkflowOrchestrationDependencyValidationException);

            return labWorkflowOrchestrationDependencyValidationException;
        }
    }
}
