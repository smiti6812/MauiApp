

using CommunityToolkit.Mvvm.ComponentModel;

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

        private DragOverlayViewModel dragOverlayViewModel;
        public DragOverlayViewModel DragOverlayViewModel
        {
            get => dragOverlayViewModel;
            set
            {
                SetProperty(ref dragOverlayViewModel, value);
            }
        }

        public ExchangeTestModelViewModel()
        {
            TestModelViewModelLocal = new TestModelViewModel();
            TestModelViewModelWorld = new TestModelViewModel();
            TestModelViewModelWorld.TestModelItems = new();
            DragOverlayViewModel = new DragOverlayViewModel();
            DragOverlayViewModel.IsVisible = false;
        }
    }
}
