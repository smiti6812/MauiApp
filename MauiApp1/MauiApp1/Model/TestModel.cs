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

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string address;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private bool isDetailsVisible = false;

        public override string ToString() => $"Name: {Name} (Age: {Age})";

    }
}
