﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Azure;
using DMX.Agent.Worker.Brokers.Blobs;
using DMX.Agent.Worker.Brokers.Loggings;
using DMX.Agent.Worker.Services.Foundations.Artifacts;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.Artifacts
{
    public partial class LabArtifactServiceTests
    {
        private readonly Mock<IBlobBroker> artifactBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILabArtifactService artifactService;

        public LabArtifactServiceTests()
        {
            this.artifactBrokerMock = new Mock<IBlobBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.artifactService = new LabArtifactService(
                this.artifactBrokerMock.Object,
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

        private static Response GetResponse()
        {
            return new Mock<Response>().Object;
        }

        private Expression<Func<Exception, bool>> SameExceptionAs(Xeption expectedException) =>
                actualException => actualException.SameExceptionAs(expectedException);

        private static string GetRandomString() =>
            new MnemonicString().GetValue();
    }
}
