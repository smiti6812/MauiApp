using System.Collections.ObjectModel;
using System.Windows.Input;

using MauiApp1.Model;

namespace MauiApp1.Utils
{
    public partial class DragDropBehavior : Behavior<View>
    {

        public static readonly BindableProperty SelectedItemsProperty =
        BindableProperty.Create(
            nameof(SelectedItems),
            typeof(ObservableCollection<object>),
            typeof(DragDropBehavior));

        public ObservableCollection<object> SelectedItems
        {
            get => (ObservableCollection<object>)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        // DROP ------------------------------

        public static readonly BindableProperty DropCommandProperty =
            BindableProperty.Create(
                nameof(DropCommand),
                typeof(ICommand),
                typeof(DragDropBehavior));

        public ICommand DropCommand
        {
            get => (ICommand)GetValue(DropCommandProperty);
            set => SetValue(DropCommandProperty, value);
        }

        public DragMessage DragMessage
        {
            get => (DragMessage)GetValue(DragMessageProperty);
            set => SetValue(DragMessageProperty, value);
        }

        private static object? dragSourceItem;
        public static readonly BindableProperty DragMessageProperty =
            BindableProperty.Create(
                nameof(DragMessage),
                typeof(DragMessage),
                typeof(DragDropBehavior),
                default(DragMessage),
                BindingMode.TwoWay
                );

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);

            if (bindable != null)
            {
                var dragGesture = new DragGestureRecognizer();
                dragGesture.DragStarting += OnDragStarting;
                bindable.GestureRecognizers.Add(dragGesture);


                var dropGesture = new DropGestureRecognizer();
                dropGesture.Drop += OnDrop;
                bindable.GestureRecognizers.Add(dropGesture);
            }
        }

        private void OnDragStarting(object? sender, Microsoft.Maui.Controls.DragStartingEventArgs e)
        {
            if (SelectedItems == null || SelectedItems.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            var data = e.Data;
            data.Properties.Add("TestModel", SelectedItems.Cast<object>().ToList());
            data.Text = $"{SelectedItems.Count} item(s)";
            // Replace the following line in OnDragStarting:
            //string joinedItems = string.Join(", ", SelectedItems.Select(item => item?.ToString() + "\n" ?? string.Empty));

            // With this line for proper line breaks in a Label:
            string joinedItems = string.Join(Environment.NewLine, SelectedItems.Select(item => item?.ToString() ?? string.Empty));
            int count = SelectedItems.Count;
            if (DragMessage is not null)
            {
                DragMessage.DragText = joinedItems;
                DragMessage.IsVisible = true;
                DragMessage.Count = count;
            }
            //WeakReferenceMessenger.Default.Send(new DragMessage { DragText = joinedItems, IsVisible = true, Count = SelectedItems.Count });


            //DragOverlayManager.Show(joinedItems);
            SelectedItems.Clear();
        }

        private void OnDrop(object? sender, DropEventArgs e)
        {
            if (DragMessage is not null)
            {
                DragMessage.DragText = string.Empty;
                DragMessage.IsVisible = false;
                DragMessage.Count = 0;
            }

            //DragMessage = new DragMessage { DragText = string.Empty, IsVisible = false };
            //WeakReferenceMessenger.Default.Send(new DragMessage { DragText = string.Empty, IsVisible = false });
            if (DropCommand?.CanExecute(e) == true)
            {
                DragOverlayManager.Hide();
                DropCommand.Execute(e);
            }
        }
    }
}
