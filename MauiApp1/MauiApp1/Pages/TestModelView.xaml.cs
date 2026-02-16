namespace MauiApp1.Pages;

public partial class TestModelView : ContentView
{
    public static readonly BindableProperty TestModelViewModelProperty =
   BindableProperty.Create(
   nameof(TestModelViewModel),
   typeof(object),
   typeof(TestModelView),
   propertyChanged: OnTestModelViewModelChanged);

    public object TestModelViewModel
    {
        get => GetValue(TestModelViewModelProperty);
        set => SetValue(TestModelViewModelProperty, value);
    }
    public TestModelView()
    {
        InitializeComponent();
    }

    private void OnPointerMoved(object sender, Microsoft.Maui.Controls.PointerEventArgs e)
    {
        //var point = e.GetPosition(null);
        //_ = WeakReferenceMessenger.Default.Send(new PointerMovedMessage(point.Value.X, point.Value.Y));
    }

    private static void OnTestModelViewModelChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is TestModelView testModelView && newValue != null)
        {
            testModelView.BindingContext = newValue;
        }
    }

    private void PointerGestureRecognizer_PointerMoved(object sender, PointerEventArgs e)
    {

    }
}