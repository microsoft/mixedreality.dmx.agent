﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Commands;
using DMX.Agent.Worker.Models.Foundations.Commands;
using DMX.Agent.Worker.Models.Foundations.Commands.Exceptions;
using Xeptions;

namespace DMX.Agent.Worker.Services.Foundations.Commands
{
    public partial class CommandService
    {
        private delegate ValueTask<string> ReturningStringFunction();

        private async ValueTask<string> TryCatch(ReturningStringFunction returningStringFunction)
        {
            try
            {
                return await returningStringFunction();
            }
            catch(EmptyCommandException emptyCommandException)
            {
                throw CreateAndLogValidationException(emptyCommandException);
            }
            catch (Win32Exception win32Exception)
            {
                var failedCommandDependencyException =
                    new FailedCommandDependencyException(win32Exception);

                throw CreateAndLogDependencyException(failedCommandDependencyException);
            }
            catch (ObjectDisposedException objectDisposedException)
            {
                var failedCommandDependencyException =
                    new FailedCommandDependencyException(objectDisposedException);

                throw CreateAndLogDependencyException(failedCommandDependencyException);
            }
            catch (PlatformNotSupportedException platformNotSupportedException)
            {
                var failedCommandDependencyException =
                    new FailedCommandDependencyException(platformNotSupportedException);

                throw CreateAndLogDependencyException(failedCommandDependencyException);
            }
            catch(InvalidOperationException invalidOprationException)
            {
                var failedCommandDependencyValidationException =
                    new FailedCommandDependencyValidationException(invalidOprationException);

                throw CreateAndLogDependencyValidationException(failedCommandDependencyValidationException);
            }
            catch (SystemException systemException)
            {
                var failedCommandDependencyException =
                    new FailedCommandDependencyException(systemException);

                throw CreateAndLogDependencyException(failedCommandDependencyException);
            }
        }

        private CommandDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var commandDependencyValidationException = new CommandDependencyValidationException(exception);
            this.loggingBroker.LogError(commandDependencyValidationException);

            return commandDependencyValidationException;
        }

        private CommandValidationException CreateAndLogValidationException(Xeption exception)
        {
            var commandValidationException = new CommandValidationException(exception);
            this.loggingBroker.LogError(commandValidationException);

            return commandValidationException;
        }

        private CommandDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var commandDependencyException = new CommandDependencyException(exception);
            this.loggingBroker.LogError(commandDependencyException);

            return commandDependencyException;
        }
    }
}
