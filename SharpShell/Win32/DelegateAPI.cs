using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpShell.Win32
{
   internal static class DelegateAPI
    {
        internal delegate IntPtr DelegateOpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);
        internal delegate IntPtr DelegateVirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, UInt32 flAllocationType, UInt32 flProtect);
        internal delegate bool DelegateWriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, out uint lpNumberOfBytesWritten);
        internal delegate bool DelegateVirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
        internal delegate IntPtr DelegateCreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        internal delegate bool DelegateCloseHandle(IntPtr hObject);
    }
}
