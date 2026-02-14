using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp1.Model
{
    public partial class TestModel : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int age;

        [ObservableProperty]
        private bool isSelected;

        public override string ToString() => $"Name: {Name} (Age: {Age})";

    }
}
