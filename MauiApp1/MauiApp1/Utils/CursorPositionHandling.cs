using System.Runtime.InteropServices;

namespace MauiApp1.Utils
{
    public static class CursorPositionHandling
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private struct POINT
        {
            public int X;
            public int Y;
        }

        public static (int screenWidth, int screenHeight) GetScreenWidthAndHeight()
        {
            int screenWidth = GetSystemMetrics(SM_CXSCREEN);
            int screenHeight = GetSystemMetrics(SM_CYSCREEN);
            return (screenWidth, screenHeight);
        }
        public static (bool leftEdge, bool rightEdge, bool topEdge, bool buttomEdge, int X, int Y) IsCursorAtScreenEdge()
        {
            var position = GetCursorPosition();
            int screenWidth = GetSystemMetrics(SM_CXSCREEN);
            int screenHeight = GetSystemMetrics(SM_CYSCREEN);

            // You can adjust the threshold as needed (e.g., 0 for exact edge, or 1-5 for near edge)
            int threshold = 0;

            bool atLeft = position.X <= threshold;
            bool atRight = position.X >= screenWidth - 1 - threshold;
            bool atTop = position.Y <= threshold;
            bool atBottom = position.Y >= screenHeight - 1 - threshold;

            return (atLeft, atRight, atTop, atBottom, position.X, position.Y);
        }
        public static (int X, int Y) GetCursorPosition()
        {
            POINT p;
            GetCursorPos(out p);
            return (p.X, p.Y);
        }
    }
}
