using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpShell.Win32
{
    internal static class FunctionPointer
    {
        [DllImport("kernel32", SetLastError = true)]
        private static extern IntPtr LoadLibraryA([MarshalAs(UnmanagedType.VBByRefStr)] ref string Name);

        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr GetProcAddress(IntPtr hProcess, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Name);

        internal static CreateApi Load<CreateApi>(string name, string method)
        {
            return (CreateApi)(object)Marshal.GetDelegateForFunctionPointer(GetProcAddress(LoadLibraryA(ref name), ref method), typeof(CreateApi));
        }
    }
}
