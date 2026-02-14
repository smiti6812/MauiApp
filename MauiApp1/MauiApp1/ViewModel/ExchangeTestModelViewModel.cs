using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using MauiApp1.Model;

using Microsoft.UI.Xaml;

namespace MauiApp1.ViewModel
{
    public partial class ExchangeTestModelViewModel : ObservableObject
    {
        private static DispatcherTimer timer;
        [ObservableProperty]
        private TestModelViewModel testModelViewModelLocal;

        [ObservableProperty]
        private TestModelViewModel testModelViewModelWorld;

        [ObservableProperty]
        private DragOverlayViewModel dragOverlayViewModel;

        public ExchangeTestModelViewModel()
        {
            TestModelViewModelLocal = new TestModelViewModel();
            TestModelViewModelWorld = new TestModelViewModel();
            TestModelViewModelWorld.TestModelItems = new();

            DragOverlayViewModel = new DragOverlayViewModel();
            DragOverlayViewModel.IsVisible = false;
            WeakReferenceMessenger.Default.Register<DragMessage>(this, (r, m) =>
            {
                if (m.IsVisible)
                {
                    DragOverlayViewModel = DragOverlayViewModel ?? new DragOverlayViewModel();
                    DragOverlayViewModel.StartTimer();
                    DragOverlayViewModel.DragText = m.DragText;
                    DragOverlayViewModel.IsVisible = m.IsVisible;
                }
                else
                {
                    DragOverlayViewModel.StopTimer();
                    DragOverlayViewModel.IsVisible = m.IsVisible;
                }
            });
        }
    }
}
