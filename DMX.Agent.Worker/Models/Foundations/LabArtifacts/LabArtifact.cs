// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.IO;

namespace DMX.Agent.Worker.Models.Foundations.LabArtifacts
{
    public class LabArtifact
    {
        public string Name { get; set; }
        public MemoryStream Content { get; set; }
    }
}
