namespace MediaLibrary.Core
{
    public sealed class Book : MediaItem
    {
        public Book(
            string name,
            string author,
            int pages,
            decimal price,
            MediaTags tags = MediaTags.None)
            : base(name, price, tags)
        {
            Author = author;
            PageCount = pages;
        }
        
        public string Author { get; }
        public int PageCount { get; }
        
        public override string ToString()
        {
            return $"Title:{Name}, author: {Author}, price: {Price}$";
        }
    }
}

