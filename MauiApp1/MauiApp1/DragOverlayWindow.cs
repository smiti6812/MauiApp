using CommunityToolkit.Mvvm.Messaging;

using MauiApp1.Model;

using Microsoft.UI.Xaml.Controls;

using Windows.Graphics;

namespace MauiApp1
{
    public class DragOverlayWindow : Microsoft.UI.Xaml.Window
    {
        private TextBlock infoText;

        public DragOverlayWindow()
        {
            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(null);

            infoText = new TextBlock
            {
                Text = "",
                Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Black),
                Padding = new Microsoft.UI.Xaml.Thickness(12),
                FontSize = 12
            };

            this.Content = infoText;
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

            appWindow.Resize(new SizeInt32(150, 150));
            WeakReferenceMessenger.Default.Register<PointerMovedMessage>(this, (r, m) =>
            {
                AppWindow?.Move(new PointInt32((int)m.X + 20, (int)m.Y + 20));
            });
        }

        public void SetText(string text) => infoText.Text = text;
    }
}
