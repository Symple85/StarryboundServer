﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;

namespace com.avilance.Starrybound
{
    internal static class NativeMethods
    {
        internal static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            int processId = Convert.ToInt32(File.ReadAllText("starbound_server.pid"));
            Process proc = Process.GetProcessById(processId);
            proc.Kill();
            File.Delete("starbound_server.pid");
            return true;
        }

        #region unmanaged
        // Declare the SetConsoleCtrlHandler function
        // as external and receiving a delegate.

        [DllImport("Kernel32")]
        internal static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        // A delegate type to be used as the handler routine
        // for SetConsoleCtrlHandler.
        internal delegate bool HandlerRoutine(CtrlTypes CtrlType);

        // An enumerated type for the control messages
        // sent to the handler routine.
        internal enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        #endregion
    }
}
