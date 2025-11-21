namespace MediaLibrary.Core
{
    public class MediaFactory
    {
        public static bool TryCreateBook(
            string name,
            string author,
            string priceText,
            out Book? book)
        {
            if (decimal.TryParse(priceText, out var price))
            {
                book = new Book(
                    name: name,
                    author: author,
                    pages: 200,
                    price: price);
                return true;
            }
            book = null;
            return false;
        }
    }
}

