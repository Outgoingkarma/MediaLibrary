namespace MediaLibrary.Core
{
    public sealed class BundleItem : MediaItem
    {

        public BundleItem(string name, decimal price, params MediaItem[] items)
            : base(name, price, MediaTags.Discounted)
        {
            Items = items;
        }
        
        public IReadOnlyList<MediaItem> Items { get; }  
    }
}

