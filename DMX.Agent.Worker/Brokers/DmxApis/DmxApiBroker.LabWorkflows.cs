// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Agent.Worker.Models.Foundations.LabWorkflows;

namespace DMX.Agent.Worker.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabWorkflowsRelativeUrl = "api/labworkflows";

        public async ValueTask<LabWorkflow> PutLabWorkflowAsync(LabWorkflow labWorkflow) =>
            await PutAsync(LabWorkflowsRelativeUrl, labWorkflow);
    }
}
