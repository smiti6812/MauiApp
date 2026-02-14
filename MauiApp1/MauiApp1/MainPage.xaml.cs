using Windows.UI.Core;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            var mauiWindow = Application.Current.Windows[0].Handler.PlatformView as Microsoft.UI.Xaml.Window;
            if (mauiWindow != null)
            {
                //mauiWindow.CoreWindow.PointerMoved += MainWindow_PointerMoved;
            }
        }

        private void OnPointerMoved(object sender, Microsoft.Maui.Controls.PointerEventArgs e)
        {
            var point = e.GetPosition((View)sender);
            /*
            var x = point.Position.X;
            var y = point.Position.Y;

            // cursor position relative to the view
            Console.WriteLine($"Mouse: X={x}, Y={y}");
            */
        }

        private void MainWindow_PointerMoved(CoreWindow sender, Windows.UI.Core.PointerEventArgs args)
        {
            MauiApp1.DragOverlayManager.UpdatePosition();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
