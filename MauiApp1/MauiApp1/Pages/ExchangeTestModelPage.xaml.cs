using MauiApp1.ViewModel;

namespace MauiApp1.Pages;

public partial class ExchangeTestModelPage : ContentPage
{
    public ExchangeTestModelPage(ExchangeTestModelViewModel exchangeTestModelViewModel)
    {
        ArgumentNullException.ThrowIfNull(exchangeTestModelViewModel);
        InitializeComponent();
        BindingContext = exchangeTestModelViewModel;
    }
}