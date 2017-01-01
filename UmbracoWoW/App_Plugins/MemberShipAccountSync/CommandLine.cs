using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace UmbracoWoW.App_Plugins.MemberShipAccountSync
{
    public static class CommandLine
    {
        public static string RunCommand(string command, string args, string workingDirectory = "")
        {
            // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            if (!string.IsNullOrWhiteSpace(workingDirectory))
            {
                p.StartInfo.WorkingDirectory = workingDirectory;
            }
            p.StartInfo.FileName = command;
            p.StartInfo.Arguments = args;
            
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            string err = p.StandardError.ReadToEnd();
            p.WaitForExit();

            if (!string.IsNullOrWhiteSpace(err))
            {
                throw new Exception(err);
            }

            return output;
        }
    }
}