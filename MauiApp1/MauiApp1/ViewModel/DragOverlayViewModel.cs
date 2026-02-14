using System.Runtime.InteropServices;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml;

namespace MauiApp1.ViewModel
{
    public partial class DragOverlayViewModel : ObservableObject
    {
        private static DispatcherTimer timer;
        [ObservableProperty]
        private string dragText = string.Empty;

        [ObservableProperty]
        private int x;

        [ObservableProperty]
        private int y;

        [ObservableProperty]
        public Rect overlayBounds;

        [ObservableProperty]
        private bool isVisible = false;

        [ObservableProperty]
        private int overlayDistance;

        public void StartTimer()
        {
            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(10); // ~60 FPS
                timer.Tick += (s, e) =>
                {
                    var position = GetCursorPosition();
                    X = (int)(position.X) - 100;
                    Y = position.Y;//OverlayDistance - position.Y;
                    OverlayBounds = new Rect(X, Y, 200, 100);
                };
            }
            timer.Start();
        }
        public void StopTimer() => timer.Stop();

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
