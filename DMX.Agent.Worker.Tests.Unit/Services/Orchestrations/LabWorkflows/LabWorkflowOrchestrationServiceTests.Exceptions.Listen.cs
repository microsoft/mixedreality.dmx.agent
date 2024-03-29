﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using DMX.Agent.Worker.Models.Orchestrations.LabWorkflows.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;
using Xunit;

namespace DMX.Agent.Worker.Tests.Unit.Services.Orchestrations.LabWorkflows
{
    public partial class LabWorkflowOrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(LabWorkflowOrchestrationDependencyValidationExceptions))]
        public void
            ShouldThrowOrchestrationDependencyValidationExceptionOnListenIfDependencyValidationErrorOccursAndLogItAsync(
                Xeption dependencyValidationException)
        {
            // given
            var expectedLabWorkflowOrchestrationDependencyValidationException =
                new LabWorkflowOrchestrationDependencyValidationException(
                    dependencyValidationException.InnerException as Xeption);

            this.labWorkflowEventServiceMock.Setup(service =>
                service.ListenToLabWorkflowEvent(this.labWorkflowOrchestrationService.ProcessLabWorkflow))
                    .Throws(dependencyValidationException);

            // when
            Action listenLabWorkflowAction =
                () => this.labWorkflowOrchestrationService.ListenToLabWorkflowEvents();

            LabWorkflowOrchestrationDependencyValidationException
                actualLabWorkflowOrchestrationDependencyValidationException =
                    Assert.Throws<LabWorkflowOrchestrationDependencyValidationException>(
                        listenLabWorkflowAction);

            // then
            actualLabWorkflowOrchestrationDependencyValidationException.Should().BeEquivalentTo(
                expectedLabWorkflowOrchestrationDependencyValidationException);

            this.labWorkflowEventServiceMock.Verify(service =>
                service.ListenToLabWorkflowEvent(
                    this.labWorkflowOrchestrationService.ProcessLabWorkflow),
                        Times.Once);

            this.loggingBroker.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowOrchestrationDependencyValidationException))),
                        Times.Once);

            this.labWorkflowEventServiceMock.VerifyNoOtherCalls();
            this.labWorkflowCommandServiceMock.VerifyNoOtherCalls();
            this.commandServiceMock.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
            this.dateTimeBroker.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(LabWorkflowOrchestrationDependencyExceptions))]
        public void ShouldThrowOrchestrationDependencyExceptionOnListenIfErrorOccursAndLogItAsync(Xeption dependencyException)
        {
            // given
            var expectedLabWorkflowOrchestrationDependencyException =
                new LabWorkflowOrchestrationDependencyException(
                    dependencyException.InnerException as Xeption);

            this.labWorkflowEventServiceMock.Setup(service =>
                service.ListenToLabWorkflowEvent(
                    this.labWorkflowOrchestrationService.ProcessLabWorkflow))
                        .Throws(dependencyException);

            // when
            Action listenLabWorkflowAction =
                () => this.labWorkflowOrchestrationService.ListenToLabWorkflowEvents();

            LabWorkflowOrchestrationDependencyException
                actualLabWorkflowOrchestrationDependencyException =
                    Assert.Throws<LabWorkflowOrchestrationDependencyException>(
                        listenLabWorkflowAction);

            // then
            actualLabWorkflowOrchestrationDependencyException.Should().BeEquivalentTo(
                expectedLabWorkflowOrchestrationDependencyException);

            this.labWorkflowEventServiceMock.Verify(service =>
                service.ListenToLabWorkflowEvent(
                    this.labWorkflowOrchestrationService.ProcessLabWorkflow),
                        Times.Once);

            this.loggingBroker.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowOrchestrationDependencyException))),
                        Times.Once);

            this.labWorkflowEventServiceMock.VerifyNoOtherCalls();
            this.labWorkflowCommandServiceMock.VerifyNoOtherCalls();
            this.commandServiceMock.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
            this.dateTimeBroker.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowOrchestrationServiceExceptionOnListenIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var exception = new Exception();

            var failedLabWorkflowOrchestrationServiceException =
                new FailedLabWorkflowOrchestrationServiceException(exception);

            var expectedLabWorkflowOrchestrationServiceException =
                new LabWorkflowOrchestrationServiceException(
                    failedLabWorkflowOrchestrationServiceException);

            this.labWorkflowEventServiceMock.Setup(service =>
                service.ListenToLabWorkflowEvent(
                    this.labWorkflowOrchestrationService.ProcessLabWorkflow))
                        .Throws(exception);

            // when
            Action listenLabWorkflowAction =
                () => this.labWorkflowOrchestrationService.ListenToLabWorkflowEvents();

            LabWorkflowOrchestrationServiceException
                actualLabWorkflowOrchestrationServiceException =
                    Assert.Throws<LabWorkflowOrchestrationServiceException>(
                        listenLabWorkflowAction);

            // then
            actualLabWorkflowOrchestrationServiceException.Should().BeEquivalentTo(
                expectedLabWorkflowOrchestrationServiceException);

            this.labWorkflowEventServiceMock.Verify(service =>
                service.ListenToLabWorkflowEvent(
                    this.labWorkflowOrchestrationService.ProcessLabWorkflow),
                        Times.Once);

            this.loggingBroker.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowOrchestrationServiceException))),
                        Times.Once);

            this.labWorkflowEventServiceMock.VerifyNoOtherCalls();
            this.labWorkflowCommandServiceMock.VerifyNoOtherCalls();
            this.commandServiceMock.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
            this.dateTimeBroker.VerifyNoOtherCalls();
        }
    }
}