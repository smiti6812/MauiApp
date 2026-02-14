using System.Collections;

namespace MauiApp1.Utils
{
    public class SyncSelectedItemsBehavior : Behavior<CollectionView>
    {
        public static readonly BindableProperty TargetSelectedItemsProperty =
            BindableProperty.Create(
                nameof(TargetSelectedItems),
                typeof(IList),
                typeof(SyncSelectedItemsBehavior),
                default(IList));

        public IList TargetSelectedItems
        {
            get => (IList)GetValue(TargetSelectedItemsProperty);
            set => SetValue(TargetSelectedItemsProperty, value);
        }

        protected override void OnAttachedTo(CollectionView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.SelectionChanged += OnSelectionChanged;
        }

        protected override void OnDetachingFrom(CollectionView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.SelectionChanged -= OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TargetSelectedItems == null)
                return;

            TargetSelectedItems.Clear();
            foreach (var item in e.CurrentSelection)
                TargetSelectedItems.Add(item);
        }
    }
}
