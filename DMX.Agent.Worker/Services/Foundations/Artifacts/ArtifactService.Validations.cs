// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Agent.Worker.Models.Foundations.Artifacts.Exceptions;

namespace DMX.Agent.Worker.Services.Foundations.Artifacts
{
    public partial class ArtifactService
    {
        public static void ValidateIfStringIsNull(string artifactName)
        {
            if (string.IsNullOrWhiteSpace(artifactName))
            {
                throw new EmptyArtifactNameException();
            }
        }
    }
}
