// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabCommands;

namespace DMX.Agent.Worker.Services.Foundations.LabCommandEvents
{
    public interface ILabCommandEventService
    {
        void ListenToLabCommandEvent(Func<LabCommand, ValueTask> labCommandEventHandler);
    }
}
