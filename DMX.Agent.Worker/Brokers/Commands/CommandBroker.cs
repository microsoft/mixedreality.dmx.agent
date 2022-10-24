// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Diagnostics;
using System.Threading.Tasks;

namespace DMX.Agent.Worker.Brokers.Commands
{
    public partial class CommandBroker : ICommandBroker
    {
        private readonly Process process;

        public CommandBroker()
        {
            this.process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
        }

        public async ValueTask<string> RunCommandAsync(string command)
        {
            this.process.Start();
            this.process.StandardInput.WriteLine(command);
            this.process.StandardInput.Flush();
            this.process.StandardInput.Close();
            await this.process.WaitForExitAsync();

            return this.process.StandardOutput.ReadToEnd();
        }
    }
}
