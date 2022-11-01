// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Brokers.Commands;
using DMX.Agent.Worker.Brokers.Loggings;

namespace DMX.Agent.Worker.Services.Foundations.Commands
{
    public class CommandService : ICommandService
    {
        private readonly ICommandBroker commandBroker;
        private readonly ILoggingBroker loggingBroker;

        public CommandService(
            ICommandBroker commandBroker,
            ILoggingBroker loggingBroker)
        {
            this.commandBroker = commandBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<string> ExecuteCommandAsync(string command)
        {
            throw new System.NotImplementedException();
        }
    }
}
