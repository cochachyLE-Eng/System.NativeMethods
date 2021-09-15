using System.Runtime.InteropServices;
using System.Text;

namespace System.NativeMethods.Clipboard
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/
    /// </summary>
    public class Text
    {
        #region Win32
        /// <summary>        
        /// Determines whether the clipboard contains data in the specified format.                
        /// </summary>                
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]                
        private static extern bool IsClipboardFormatAvailable(uint format);
        /// <summary>   
        /// Retrieves data from the clipboard in a specified format. The clipboard must have been opened previously.
        /// </summary>   
        [DllImport("User32.dll", SetLastError = true)]
        private static extern IntPtr GetClipboardData(uint uFormat);
        /// <summary>   
        /// Opens the clipboard for examination and prevents other applications from modifying the clipboard content.
        /// </summary>   
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);
        /// <summary>   
        /// Closes the clipboard.
        /// </summary>   
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]        
        private static extern bool CloseClipboard();
        /// <summary>   
        /// Locks a global memory object and returns a pointer to the first byte of the object's memory block.
        /// </summary>   
        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr GlobalLock(IntPtr hMem);
        /// <summary>   
        /// Decrements the lock count associated with a memory object that was allocated with GMEM_MOVEABLE.
        /// </summary>   
        [DllImport("Kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GlobalUnlock(IntPtr hMem);
        /// <summary>   
        /// Retrieves the current size of the specified global memory object, in bytes.
        /// </summary>   
        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern int GlobalSize(IntPtr hMem);

        private const uint CF_UNICODETEXT = 13U;

        #endregion

        public static string GetText()
        {
            if (!IsClipboardFormatAvailable(CF_UNICODETEXT))
                return null;

            try
            {
                if (!OpenClipboard(IntPtr.Zero))
                    return null;

                IntPtr handle = GetClipboardData(CF_UNICODETEXT);
                if (handle == IntPtr.Zero)
                    return null;

                IntPtr pointer = IntPtr.Zero;

                try
                {
                    pointer = GlobalLock(handle);
                    if (pointer == IntPtr.Zero)
                        return null;

                    int size = GlobalSize(handle);
                    byte[] buff = new byte[size];

                    Marshal.Copy(pointer, buff, 0, size);

                    return Encoding.Unicode.GetString(buff).TrimEnd('\0');
                }
                finally
                {
                    if (pointer != IntPtr.Zero)
                        GlobalUnlock(handle);
                }
            }
            finally
            {
                CloseClipboard();
            }
        }
    }
}
