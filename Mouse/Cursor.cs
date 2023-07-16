using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vaetech.NativeMethods.Mouse
{
    public static class Cursor
    {        
        /// <summary>
        /// <para>Get the position of Cursor/Mouse.</para>
        /// <example>        
        /// <b>Method 1:</b><br/>
        /// Point defPnt = new Point();<br/>
        /// GetCursorPos(ref defPnt);        
        /// </example>
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>        
        [DllImport("user32.dll")]
        private static extern BOOL GetCursorPos(out Point pt);
        [DllImport("user32.dll")]
        private static extern BOOL SetCursorPos([In] int x, [In] int y);

        [DllImport("user32.dll")]
        public static extern bool ShowCursor(bool bShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ClipCursor([In] ref RECT rcClip);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ClipCursor([In] IntPtr rcClip);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetClipCursor([Out] out RECT rcClip);

        public static bool MoveCursor(int x, int y)
        {
            return SetCursorPos(x, y) != BOOL.False;
        }

        public static bool TryGetPosition(out Point point)
        {
            return GetCursorPos(out point) != BOOL.False;
        }

        public static Rectangle GetClip()
        {
            if (!GetClipCursor(out var rect))
            {
                var error = Marshal.GetLastWin32Error();
                throw new Win32Exception(error);
            }

            return rect;
        }

        public static void SetClip(in Rectangle value)
        {
            var rect = (RECT)value;
            if (!ClipCursor(ref rect))
            {
                var error = Marshal.GetLastWin32Error();
                throw new Win32Exception(error);
            }
        }

        public static void ClearClip()
        {
            if (!ClipCursor(IntPtr.Zero))
            {
                var error = Marshal.GetLastWin32Error();
                throw new Win32Exception(error);
            }
        }

        #region Create custom cursor
        [DllImport("user32.dll")]
        private static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        /// <summary>
        /// Create a custom cursor from a bitmap. <br/>
        /// <example>
        /// <b>Use in NetFramework:</b><br/>
        /// System.Windows.Forms.Cursor = new System.Windows.Forms.Cursor(CreateCursor(bitmap, 3, 3));
        /// </example>
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="xHotSpot"></param>
        /// <param name="yHotSpot"></param>
        /// <returns></returns>
        public static IntPtr CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IconInfo tmp = new IconInfo();
            GetIconInfo(bmp.GetHicon(), ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            return CreateIconIndirect(ref tmp);
        }
        #endregion

        public enum BOOL : uint
        {
            False = 0,
            True = 1,
        }
        public struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator RECT(in Rectangle r)
            {
                return new RECT
                {
                    Left = r.Left,
                    Top = r.Top,
                    Right = r.Right,
                    Bottom = r.Bottom
                };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Rectangle(in RECT r)
            {
                return Rectangle.FromLTRB(r.Left, r.Top, r.Right, r.Bottom);
            }
        }
        
    }
}
