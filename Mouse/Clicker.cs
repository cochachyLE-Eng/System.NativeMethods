namespace Vaetech.NativeMethods.Mouse
{
    public class Clicker
    {
        /// <summary>
        /// <para>Produce mouse event.</para>
        /// <example>
        /// <b><u>Samples:</u></b><br/>
        /// mouse_event(MOUSEEVENT_LEFTDOWN, 0, 0, 0, 0); <br/>
        /// mouse_event(MOUSEEVENT_LEFTUP, 0, 0, 0, 0); <br/>
        /// mouse_event(MOUSEEVENT_MIDDLEDOWN, 0, 0, 0, 0); <br/>
        /// mouse_event(MOUSEEVENT_MIDDLEUP, 0, 0, 0, 0); <br/>
        /// mouse_event(MOUSEEVENT_RIGHTDOWN, 0, 0, 0, 0); <br/>
        /// mouse_event(MOUSEEVENT_RIGHTUP, 0, 0, 0, 0); <br/>
        /// </example>
        /// </summary>
        /// <param name="dwFlags"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="cButtons"></param>
        /// <param name="dwExtraInfo"></param>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENT_LEFTDOWN = 0x02;
        public const int MOUSEEVENT_LEFTUP = 0x04;
        public const int MOUSEEVENT_MIDDLEDOWN = 0x20;
        public const int MOUSEEVENT_MIDDLEUP = 0x40;
        public const int MOUSEEVENT_RIGHTDOWN = 0x08;
        public const int MOUSEEVENT_RIGHTUP = 0x10;
    }
}
