
using MauiApp1.ViewModel;

namespace MauiApp1.Pages;

public partial class AbsolutLayoutDragOverlayView : ContentView
{
    public static readonly BindableProperty DragOverlayViewModelProperty =
        BindableProperty.Create(
        nameof(DragOverlayViewModel),
        typeof(object),
        typeof(AbsolutLayoutDragOverlayView),
        propertyChanged: OnDragOverlayViewModelChanged);

    public object DragOverlayViewModel
    {
        get => GetValue(DragOverlayViewModelProperty);
        set => SetValue(DragOverlayViewModelProperty, value);
    }

    public static readonly BindableProperty OverlayDistanceProperty =
    BindableProperty.Create(
        nameof(OverlayDistance),
        typeof(double),
        typeof(AbsolutLayoutDragOverlayView),
        0.0,
        propertyChanged: OnOverlayHeightChanged);

    public double OverlayDistance
    {
        get => (double)GetValue(OverlayDistanceProperty);
        set => SetValue(OverlayDistanceProperty, value);
    }

    public static Rect GetAbsoluteLayoutBounds()
    {
        var bounds = AbsoluteLayout.GetLayoutBounds(null);
        return bounds;
    }

    public AbsolutLayoutDragOverlayView()
    {
        InitializeComponent();
    }

    private static void OnOverlayHeightChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is AbsolutLayoutDragOverlayView dragOverlayView && newValue is double newHeight)
        {
            if (dragOverlayView.BindingContext is DragOverlayViewModel dragOverlayViewModel)
            {
                dragOverlayViewModel.OverlayDistance = (int)newHeight;
            }
        }
    }

    private static void OnDragOverlayViewModelChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is AbsolutLayoutDragOverlayView dragOverlayViewModel && newValue != null)
        {
            dragOverlayViewModel.BindingContext = newValue;
        }
    }
}