// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.LabCommands;
using DMX.Agent.Worker.Models.LabCommands.Exceptions;

namespace DMX.Agent.Worker.Services.Foundations.LabCommandEvents
{
    public partial class LabCommandEventService
    {
        private void ValidateLabCommandIsNotNull(LabCommand labCommand)
        {
            if (labCommand is null)
            {
                throw new NullLabCommandHandlerException();
            }
        }
    }
}
