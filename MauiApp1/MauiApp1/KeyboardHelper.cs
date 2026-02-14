using System.Runtime.InteropServices;

namespace MauiApp1
{
    public static class KeyboardHelper
    {
        [DllImport("user32.dll")]
        private static extern short GetKeyState(int nVirtKey);

        private const int VK_CONTROL = 0x11;
        private const int VK_SHIFT = 0x10;

        public static bool IsCtrlPressed()
            => (GetKeyState(VK_CONTROL) & 0x8000) != 0;

        public static bool IsShiftPressed()
            => (GetKeyState(VK_SHIFT) & 0x8000) != 0;
    }
}
