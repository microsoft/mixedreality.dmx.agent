// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.LabArtifacts.Exceptions;

namespace DMX.Agent.Worker.Services.Foundations.Artifacts
{
    public partial class ArtifactService
    {
        public static void ValidateIfStringIsNull(string artifactName, string filepath)
        {
            if (string.IsNullOrWhiteSpace(artifactName) || string.IsNullOrWhiteSpace(filepath))
            {
                throw new EmptyLabArtifactNameException();
            }
        }
    }
}
