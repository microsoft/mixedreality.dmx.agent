// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using DMX.Agent.Worker.Models.LabCommands.Exceptions;
using Xeptions;

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
            catch (Exception exception)
            {
                var failedLabCommandServiceException = new FailedLabCommandServiceException(exception);

                throw CreateAndLogServiceException(failedLabCommandServiceException);
            }
        }

        private Xeption CreateAndLogServiceException(Xeption exception)
        {
            var labCommandServiceException = new LabCommandServiceException(exception);
            this.loggingBroker.LogError(labCommandServiceException);

            return labCommandServiceException;
        }
    }
}
