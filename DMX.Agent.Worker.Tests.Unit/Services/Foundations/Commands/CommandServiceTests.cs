﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Brokers.Commands;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Services.Foundations.Commands;
using Moq;
using Tynamix.ObjectFiller;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.Commands
{
    public partial class CommandServiceTests
    {
        private readonly Mock<ICommandBroker> commandBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICommandService commandService;

        public CommandServiceTests()
        {
            this.commandBrokerMock = new Mock<ICommandBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.commandService = new CommandService(
                this.commandBrokerMock.Object,
                this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();
    }
}
