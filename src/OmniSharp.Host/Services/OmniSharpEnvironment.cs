﻿using System;
using Microsoft.Extensions.Logging;

namespace OmniSharp.Services
{
    public class OmniSharpEnvironment : IOmniSharpEnvironment
    {
        public string Path { get; }
        public string SolutionFilePath { get; }
        public string SharedDirectoryPath { get; }
        public int Port { get; }
        public int HostPID { get; }
        public LogLevel TraceType { get; }
        public TransportType TransportType { get; }
        public string[] OtherArgs { get; }

        public OmniSharpEnvironment(
            string path = null,
            int port = -1,
            int hostPid = -1,
            LogLevel traceType = LogLevel.None,
            TransportType transportType = TransportType.Stdio,
            string[] otherArgs = null)
        {
            path = path ?? ".";

            if (System.IO.Path.GetExtension(path).Equals(".sln", StringComparison.OrdinalIgnoreCase))
            {
                SolutionFilePath = path;
                Path = System.IO.Path.GetDirectoryName(path);
            }
            else
            {
                Path = path;
            }

            Port = port;
            HostPID = hostPid;
            TraceType = traceType;
            TransportType = transportType;
            OtherArgs = otherArgs;

            // On Windows: %APPDATA%\.omnisharp\omnisharp.json
            // On Mac/Linux: ~/.omnisharp/omnisharp.json
            var root = Environment.GetEnvironmentVariable("USERPROFILE") ??
                Environment.GetEnvironmentVariable("HOME");

            if (root != null)
            {
                SharedDirectoryPath = System.IO.Path.Combine(root, ".omnisharp");
            }
        }
    }
}