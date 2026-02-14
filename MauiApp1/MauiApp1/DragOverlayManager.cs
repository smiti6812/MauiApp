using System.Runtime.InteropServices;

using Microsoft.UI.Xaml;

using Windows.Graphics;

namespace MauiApp1
{
    public static class DragOverlayManager
    {
        private static DragOverlayWindow window;
        private static DispatcherTimer timer;

        public static void Show(string text)
        {
            if (window == null)
            {
                window = new DragOverlayWindow();
                window.Activate();
            }
            window.SetText(text);

            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
                timer.Tick += (s, e) => UpdatePosition();
            }
            timer.Start();
        }

        public static void Hide()
        {
            timer?.Stop();
            window?.Close();
            window = null;
        }

        public static void UpdatePosition()
        {
            if (window == null)
                return;

            var pos = GetCursorPosition();
            window.AppWindow.Move(new PointInt32(pos.X + 20, pos.Y + 20));
        }

        private static (int X, int Y) GetCursorPosition()
        {
            POINT p;
            GetCursorPos(out p);
            return (p.X, p.Y);
        }

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        private struct POINT
        {
            public int X;
            public int Y;
        }
    }
}
