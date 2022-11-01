// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Commands;

namespace DMX.Agent.Worker.Services.Foundations.Commands
{
    public partial class CommandService
    {
        private static void ValidateIfStringIsNull(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                throw new EmptyCommandException();
            }
        }
    }
}
