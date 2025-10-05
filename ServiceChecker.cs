using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace ServerManager
{
    internal static class ServiceChecker
    {
        public enum TargetType { WindowsService, Process }

        public sealed class Target
        {
            /// <summary>Friendly key you’ll use in code (e.g. "GameServer").</summary>
            public string Key { get; init; } = "";
            /// <summary>How to check this target.</summary>
            public TargetType Type { get; init; }
            /// <summary>
            /// For WindowsService: candidate service names (first existing and Running counts as OK).
            /// For Process: process names WITHOUT .exe (any running counts as OK).
            /// </summary>
            public string[] Names { get; init; } = Array.Empty<string>();
        }

        /// <summary>Check a single target.</summary>
        public static bool IsRunning(Target target)
        {
            return target.Type switch
            {
                TargetType.WindowsService => IsAnyServiceRunning(target.Names),
                TargetType.Process => IsAnyProcessRunning(target.Names),
                _ => false
            };
        }

        /// <summary>Check many targets at once. Returns a map of Key => Running.</summary>
        public static Dictionary<string, bool> CheckAll(IEnumerable<Target> targets)
        {
            var result = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
            foreach (var t in targets)
                result[t.Key] = IsRunning(t);
            return result;
        }

        private static bool IsAnyServiceRunning(IEnumerable<string> serviceNames)
        {
            try
            {
                // Pull the services once for efficiency
                var all = ServiceController.GetServices();
                foreach (var name in serviceNames)
                {
                    var svc = all.FirstOrDefault(s =>
                        s.ServiceName.Equals(name, StringComparison.OrdinalIgnoreCase));
                    if (svc != null && svc.Status == ServiceControllerStatus.Running)
                        return true;
                }
            }
            catch
            {
                // Ignore and treat as not running
            }
            return false;
        }

        private static bool IsAnyProcessRunning(IEnumerable<string> processNames)
        {
            try
            {
                foreach (var name in processNames)
                {
                    // Process.GetProcessesByName takes the name WITHOUT ".exe"
                    if (Process.GetProcessesByName(name).Length > 0)
                        return true;
                }
            }
            catch
            {
                // Ignore and treat as not running
            }
            return false;
        }
    }
}
