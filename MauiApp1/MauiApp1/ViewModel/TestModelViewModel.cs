using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MauiApp1.Model;

namespace MauiApp1.ViewModel
{
    public partial class TestModelViewModel : ObservableObject
    {
        private int? lastSelectedIndex;
        public ObservableCollection<TestModel> TestModelItems { get; set; } =
            new()
            {
                new TestModel { Name = "Alpha", Age = 32, Address = "123 Alpha St", Description = "First entry", Email = "alpha@example.com" },
                new TestModel { Name = "Bravo", Age = 23, Address = "456 Bravo Ave", Description = "Second entry", Email = "bravo@example.com" },
                new TestModel { Name = "Charlie", Age = 44, Address = "789 Charlie Blvd", Description = "Third entry", Email = "charlie@example.com" },
                new TestModel { Name = "Delta", Age = 52, Address = "321 Delta Rd", Description = "Fourth entry", Email = "delta@example.com" },
                new TestModel { Name = "Maci", Age = 63, Address = "654 Maci Ln", Description = "Fifth entry", Email = "maci@example.com" },
                new TestModel { Name = "Laci", Age = 15, Address = "987 Laci Ct", Description = "Sixth entry", Email = "laci@example.com" }
            };


        private ObservableCollection<object> _selectedItems;
        public ObservableCollection<object> SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        public ICommand DropCommand { get; }
        public ICommand ItemTappedCommand { get; }
        public ICommand ShowDetailsCommand { get; }
        public TestModelViewModel()
        {
            DropCommand = new Command<DropEventArgs>(OnDrop);
            ItemTappedCommand = new RelayCommand<TestModel>(OnItemTapped);
            SelectedItems = new();
            ShowDetailsCommand = new RelayCommand<TestModel>(ShowDetails);
        }

        private void ShowDetails(TestModel model)
        {
            model.IsDetailsVisible = !model.IsDetailsVisible;
        }

        private void OnItemTapped(TestModel tappedItem)
        {
            /*
            tappedItem.IsSelected = !tappedItem.IsSelected;

            if (tappedItem.IsSelected && !SelectedItems.Contains(tappedItem))
            {
                SelectedItems.Add(tappedItem);
            }
            else
            {
                SelectedItems.Remove(tappedItem);
            }
            */
            var index = TestModelItems.IndexOf(tappedItem);
            bool ctrlPressed = KeyboardHelper.IsCtrlPressed();
            bool shiftPressed = KeyboardHelper.IsShiftPressed();

            if (ctrlPressed && shiftPressed && lastSelectedIndex.HasValue)
            {
                // Range selection (Ctrl + Shift): add range to current selection
                int start = Math.Min(lastSelectedIndex.Value, index);
                int end = Math.Max(lastSelectedIndex.Value, index);

                for (int i = start; i <= end; i++)
                {
                    var item = TestModelItems[i];
                    if (!SelectedItems.Contains(item))
                    {
                        item.IsSelected = true;
                        SelectedItems.Add(item);
                    }
                }
            }
            else if (ctrlPressed)
            {
                // Multi-select (Ctrl only): toggle selection, keep others
                tappedItem.IsSelected = !tappedItem.IsSelected;
                if (tappedItem.IsSelected)
                    SelectedItems.Add(tappedItem);
                else
                    SelectedItems.Remove(tappedItem);

                lastSelectedIndex = index;
            }
            else
            {
                // Single selection (clear previous)
                foreach (var item in TestModelItems)
                    item.IsSelected = false;
                SelectedItems.Clear();

                tappedItem.IsSelected = true;
                SelectedItems.Add(tappedItem);

                lastSelectedIndex = index;
            }
        }

        private void OnDrop(DropEventArgs e)
        {
            DragOverlayManager.Hide();
            if (!e.Data.Properties.TryGetValue("TestModel", out var data))
                return;

            if (data is List<object> items)
            {
                foreach (var item in items.OfType<TestModel>())
                {
                    item.IsSelected = false;
                    var newItem = new TestModel
                    {
                        Name = item.Name,
                        Age = item.Age,
                        Address = item.Address,
                        Email = item.Email,
                        Description = item.Description,
                        IsSelected = false
                    };
                    TestModelItems.Add(newItem);
                }
            }
            else if (data is TestModel singleItem)
            {
                var newItem = new TestModel
                {
                    Name = singleItem.Name,
                    Age = singleItem.Age,
                    IsSelected = false
                };
                TestModelItems.Add(newItem);
            }
        }
    }
}
