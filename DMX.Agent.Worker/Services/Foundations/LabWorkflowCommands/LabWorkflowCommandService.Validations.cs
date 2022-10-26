// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Data;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands;
using DMX.Agent.Worker.Models.Foundations.LabWorkflowCommands.Exceptions;

namespace DMX.Agent.Worker.Services.Foundations.LabWorkflowCommands
{
    public partial class LabWorkflowCommandService : ILabWorkflowCommandService
    {
        private void ValidateLabWorkflowOnModify(LabWorkflowCommand labWorkflowCommand)
        {
            ValidateLabWorkflowCommandIsNotNull(labWorkflowCommand);

            Validate(
                (Rule: IsInvalid(labWorkflowCommand.Id), Parameter: nameof(LabWorkflowCommand.Id)),
                (Rule: IsInvalid(labWorkflowCommand.LabId), Parameter: nameof(LabWorkflowCommand.LabId)),
                (Rule: IsInvalid(labWorkflowCommand.WorkflowId), Parameter: nameof(LabWorkflowCommand.WorkflowId)),
                (Rule: IsInvalid(labWorkflowCommand.Arguments), Parameter: nameof(LabWorkflowCommand.Arguments)),
                (Rule: IsInvalid(labWorkflowCommand.Results), Parameter: nameof(LabWorkflowCommand.Results)),
                (Rule: IsInvalid(labWorkflowCommand.CreatedDate), Parameter: nameof(LabWorkflowCommand.CreatedDate)),
                (Rule: IsInvalid(labWorkflowCommand.UpdatedDate), Parameter: nameof(LabWorkflowCommand.UpdatedDate)),
                (Rule: IsInvalid(labWorkflowCommand.CreatedBy), Parameter: nameof(LabWorkflowCommand.CreatedBy)),
                (Rule: IsInvalid(labWorkflowCommand.UpdatedBy), Parameter: nameof(LabWorkflowCommand.UpdatedBy)));
        }

        private static void ValidateLabWorkflowCommandIsNotNull(LabWorkflowCommand labWorkflowCommand)
        {
            if (labWorkflowCommand is null)
            {
                throw new NullLabWorkflowCommandException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(ulong userId) => new
        {
            Condition = userId == default,
            Message = "User is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidLabWorkflowException = new InvalidLabWorkflowCommandException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidLabWorkflowException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidLabWorkflowException.ThrowIfContainsErrors();
        }
    }
}
