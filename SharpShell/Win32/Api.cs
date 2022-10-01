using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharpShell.Win32.DelegateAPI;

namespace SharpShell.Win32
{
    internal static class Api
    {
        internal static readonly DelegateOpenProcess OpenProcess = FunctionPointer.Load<DelegateOpenProcess>("kernel32", "OpenProcess");
        internal static readonly DelegateVirtualAllocEx VirtualAllocEx = FunctionPointer.Load<DelegateVirtualAllocEx>("kernel32", "VirtualAllocEx");
        internal static readonly DelegateWriteProcessMemory WriteProcessMemory = FunctionPointer.Load<DelegateWriteProcessMemory>("kernel32", "WriteProcessMemory");
        internal static readonly DelegateVirtualProtectEx VirtualProtectEx = FunctionPointer.Load<DelegateVirtualProtectEx>("kernel32", "VirtualProtectEx");
        internal static readonly DelegateCreateRemoteThread CreateRemoteThread = FunctionPointer.Load<DelegateCreateRemoteThread>("kernel32", "CreateRemoteThread");
        internal static readonly DelegateCloseHandle CloseHandle = FunctionPointer.Load<DelegateCloseHandle>("kernel32", "CloseHandle");
    }
}
