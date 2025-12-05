namespace MediaLibrary.Core
{
    public static class MediaExtensions
    {
        public static IEnumerable<T> WithTag<T>(this IEnumerable<T> source, MediaTags tag)
            where T : MediaItem
        {
            foreach (var item in source)
            {
                if (item.HasTag(tag))
                {
                    yield return item;
                }
            }
        }
        public static string CapitalizeTitle(this string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return title;
            }
            var trimmed = title.Trim();
            return char.ToUpper(trimmed[0]) + trimmed[1..];
        }

        public static void Deconstruct(this Book book, out string title, out string author, out int pages)
        {
            title = book.Name;
            author = book.Author;
            pages = book.PageCount;
        }
    }
}