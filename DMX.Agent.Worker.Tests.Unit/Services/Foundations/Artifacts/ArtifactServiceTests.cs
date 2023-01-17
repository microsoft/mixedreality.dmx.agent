// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Brokers.Blobs;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Models.Foundations.Artifacts.Exceptions;
using DMX.Agent.Worker.Services.Foundations.Artifacts;
using Moq;
using System.Linq.Expressions;
using System;
using Tynamix.ObjectFiller;
using Xeptions;
using Azure;
using Xunit;
using Azure.Storage.Blobs.Models;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.Artifacts
{
    public partial class ArtifactServiceTests
    {
        private readonly Mock<IBlobBroker> ArtifactBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IArtifactService ArtifactService;

        public ArtifactServiceTests()
        {
            this.ArtifactBrokerMock = new Mock<IBlobBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.ArtifactService = new ArtifactService(
                this.ArtifactBrokerMock.Object,
                this.loggingBrokerMock.Object);
        }

        public static TheoryData<Exception> DependencyExceptions()
        {
            string randomMessage = GetRandomString();

            return new TheoryData<Exception>
            {
                new RequestFailedException(randomMessage)
            };
        }

        private Expression<Func<Exception, bool>> SameExceptionAs(Xeption expectedException) =>
                actualException => actualException.SameExceptionAs(expectedException);

        private static string GetRandomString() =>
            new MnemonicString().GetValue();
    }
}
