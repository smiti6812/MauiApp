using System.Runtime.InteropServices;

using CommunityToolkit.Mvvm.ComponentModel;

using MauiApp1.Model;

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
        private bool isVisible;

        [ObservableProperty]
        private int overlayDistance;

        [ObservableProperty]
        private DragMessage dragMessage;

        [ObservableProperty]
        private int count;

        public void StartTimer()
        {
            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(10); // ~60 FPS
                timer.Tick += (s, e) =>
                {
                    var widthHeight = Utils.CursorPositionHandling.GetScreenWidthAndHeight();
                    var position = GetCursorPosition();
                    X = (int)(((double)widthHeight.screenWidth) / 2) - 100;//(int)(position.X) - 100;
                    int offset = position.Y < 300 ? 300 : 400;
                    Y = position.Y - (OverlayDistance + offset);//OverlayDistance - position.Y;
                    OverlayBounds = new Rect(X, Y, 200, (count * 25) + 60);
                };
            }
            timer.Start();
        }
        public void StopTimer()
        {
            timer?.Stop();
            timer = null;
        }

        private static (int X, int Y) GetCursorPosition()
        {
            POINT p;
            GetCursorPos(out p);
            return (p.X, p.Y);
        }

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);
        internal void OnNavigatedFrom(object? sender, NavigatedFromEventArgs e)
        {
            //WeakReferenceMessenger.Default.Unregister<DragMessage>(this);
        }
        internal void OnNavigatedTo(object? sender, NavigatedToEventArgs e)
        {
            IsVisible = false;
            /*
            WeakReferenceMessenger.Default.Register<DragMessage>(this, (r, m) =>
            {
                if (m.IsVisible)
                {
                    StartTimer();
                    DragText = m.DragText;
                    IsVisible = m.IsVisible;
                    Count = m.Count;
                }
                else
                {
                    StopTimer();
                    IsVisible = m.IsVisible;
                }
            });
            */
        }

        internal void OnDragMessageIsVisibleChanged(object? sender, DragMessage msg)
        {
            IsVisible = msg.IsVisible;
            DragMessage = msg;
            if (IsVisible)
            {
                StartTimer();
                if (!string.IsNullOrWhiteSpace(msg.DragText))
                {
                    DragText = DragMessage.DragText;
                    IsVisible = DragMessage.IsVisible;
                    Count = DragMessage.Count;
                }
            }
            else
            {
                StopTimer();
            }
        }

        private struct POINT
        {
            public int X;
            public int Y;
        }
    }
}
