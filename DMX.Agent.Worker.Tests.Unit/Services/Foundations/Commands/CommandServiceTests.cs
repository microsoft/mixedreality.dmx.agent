// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using DMX.Agent.Worker.Brokers.Commands;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Services.Foundations.Commands;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

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

        public static TheoryData DependencyExceptions()
        {
            string randomMessage = GetRandomString();

            return new TheoryData<Exception>
            {
                new Win32Exception(),
                new ObjectDisposedException(randomMessage),
                new PlatformNotSupportedException(),
                new SystemException(),
            };
        }

        public static TheoryData DependencyValidationExceptions()
        {
            return new TheoryData<Exception>
            {
                new InvalidOperationException(),
            };
        }

        private Expression<Func<Exception, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static string GetRandomString() =>
            new MnemonicString().GetValue();
    }
}
