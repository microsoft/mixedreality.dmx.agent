// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommandEvents;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using FluentAssertions;
using LeVent.Models.Clients.Exceptions;
using Moq;
using Xeptions;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Foundations.LabWorkflowCommandEvents
{
    public partial class LabWorkflowCommandEventServiceTests
    {
        [Fact]
        public void ShouldThrowDependencyValidationOnRegisterIfDependencyValidationErrorOccursAndLogIt()
        {
            // given
            Func<LabWorkflowCommand, ValueTask> nullLabWorkflowCommandEventHandler = 
                Mock.Of<Func<LabWorkflowCommand, ValueTask>>();    
            
            string someEventName = GetRandomEventName();
            var someException = new Xeption();

            var leventValidationException =
                new LeVentValidationException(someException);

            var invalidLabWorkflowCommandEventHandlerException =
                new InvalidLabWorkflowCommandEventHandlerException(
                    leventValidationException);

            var expectedLabWorkflowCommandEventDependencyValidationException =
                new LabWorkflowCommandEventDependencyValidationException(
                    invalidLabWorkflowCommandEventHandlerException);

            this.eventBrokerMock.Setup(broker =>
                broker.ListenToLabWorkflowCommandEvent(
                    It.IsAny<Func<LabWorkflowCommand, ValueTask>>(),
                    It.IsAny<string>()))
                        .Throws(leventValidationException);

            // when
            Action registerLabWorkflowCommandEventHandlerAction = () =>
                this.labWorkflowCommandEventService.RegisterLabWorkflowCommandEventHandler(
                    nullLabWorkflowCommandEventHandler,
                    someEventName);

            LabWorkflowCommandEventDependencyValidationException 
                actualLabWorkflowCommandEventDependencyValidationException =
                    Assert.Throws<LabWorkflowCommandEventDependencyValidationException>(
                        registerLabWorkflowCommandEventHandlerAction);

            // then
            actualLabWorkflowCommandEventDependencyValidationException.Should().BeEquivalentTo(
                expectedLabWorkflowCommandEventDependencyValidationException);

            this.eventBrokerMock.Verify(broker =>
                broker.ListenToLabWorkflowCommandEvent(
                    It.IsAny<Func<LabWorkflowCommand, ValueTask>>(),
                    It.IsAny<string>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowCommandEventDependencyValidationException))),
                        Times.Once);

            this.eventBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
