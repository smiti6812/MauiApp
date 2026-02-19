using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp1.Model
{
    public partial class DragMessage : ObservableObject
    {
        [ObservableProperty]
        private string dragText;

        [ObservableProperty]
        private bool isVisible;

        [ObservableProperty]
        private int count;
    }
}
