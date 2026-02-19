

using CommunityToolkit.Mvvm.ComponentModel;

using MauiApp1.Model;

using Microsoft.UI.Xaml;

namespace MauiApp1.ViewModel
{
    public partial class ExchangeTestModelViewModel : ObservableObject
    {
        public event EventHandler<DragMessage>? DragMessageIsVisibleChanged;

        private static DispatcherTimer timer;
        [ObservableProperty]
        private TestModelViewModel testModelViewModelLocal;

        [ObservableProperty]
        private TestModelViewModel testModelViewModelWorld;

        [ObservableProperty]
        private DragMessage dragMessage = new();

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
            TestModelViewModelLocal = new TestModelViewModel(DragMessage);
            TestModelViewModelWorld = new TestModelViewModel(DragMessage);
            TestModelViewModelWorld.TestModelItems = new();
            DragOverlayViewModel = new DragOverlayViewModel();
            DragOverlayViewModel.IsVisible = false;
            DragMessageIsVisibleChanged += DragOverlayViewModel.OnDragMessageIsVisibleChanged;
            DragMessage.PropertyChanged += DragMessage_PropertyChanged;
        }

        private void DragMessage_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DragMessage.IsVisible) || e.PropertyName == nameof(DragMessage.DragText) || e.PropertyName == nameof(DragMessage.Count))
            {
                DragMessageIsVisibleChanged?.Invoke(this, DragMessage);
            }
        }
    }
}
