// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.Commands
{
    public partial class CommandServiceTests
    {
        [Fact]
        public async Task ShouldExecuteCommandAsync()
        {
            // given
            string randomString = GetRandomString();
            string inputCommandString = randomString;
            string responseString = GetRandomString();
            string expectedResponseString = responseString.DeepClone();

            this.commandBrokerMock.Setup(broker =>
                broker.RunCommandAsync(inputCommandString))
                    .ReturnsAsync(responseString);

            // when
            string actualResponseString =
                await this.commandService.ExecuteCommandAsync(inputCommandString);

            // then
            actualResponseString.Should().BeEquivalentTo(expectedResponseString);

            this.commandBrokerMock.Verify(broker =>
                broker.RunCommandAsync(inputCommandString),
                    Times.Once);

            this.commandBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
