﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

namespace DMX.Agent.Worker.Models.LabCommands
{
    public enum CommandStatus
    {
        Pending,
        Running,
        Aborted,
        Completed,
        Error
    }
}