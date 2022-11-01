// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Commands;
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
        }

        private CommandValidationException CreateAndLogValidationException(Xeption exception)
        {
            var commandValidationException = new CommandValidationException(exception);
            this.loggingBroker.LogError(commandValidationException);

            return commandValidationException;
        }
    }
}
