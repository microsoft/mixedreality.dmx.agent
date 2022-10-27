// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabCommands;
using DMX.Agent.Worker.Models.Foundations.LabCommands.Exceptions;

namespace DMX.Agent.Worker.Services.Foundations.LabCommandEvents
{
    public partial class LabCommandEventService
    {
        private void ValidateLabCommandEventHandler(Func<LabCommand, ValueTask> labCommandHandler)
        {
            if (labCommandHandler is null)
            {
                throw new NullLabCommandHandlerException();
            }
        }
    }


}
