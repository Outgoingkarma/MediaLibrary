

using System.Linq;
using MediaLibrary.Core;

namespace MediaLibrary.App
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var items = new List<MediaItem>();

            if (MediaFactory.TryCreateBook("The Hobbit".CapitalizeTitle(), "", "123", out var hobbit))
            {
                items.Add(hobbit);
            }


            var ddd = new Book(
                name: "Clean code".CapitalizeTitle(),
                author: "Uncle bob",
                pages: 216,
                price: 39.88m,
                tags: MediaTags.New | MediaTags.Popular);
            items.Add(ddd);
            
            
            var bundled = new BundleItem("Starter pack", 39.99m, hobbit, ddd);
            items.Add(bundled);
            
            var repository = new MediaRepository<MediaItem>();
            repository.ItemAdded += (_, item) => Console.WriteLine($"[event] Added {item.Name} to repository");


            foreach (var item in items)
            {
                repository.Add(item);
            }

            try
            {
                repository.Add(new Book("Faulty", "Unknown", 10, -1m));
            }
            catch (InvalidMediaException ex)
            {
                Console.WriteLine($"Could not add item: {ex.Message}");
            }
            
            Predicate<MediaItem> expensivePredicate = item => item.Price > 30m;
            var expensiveItems = items.FindAll(expensivePredicate);
            
            
            foreach (var item in repository)
            {
                switch (item)
                {
                    case Book b when b.PageCount > 400:
                        Console.WriteLine($"Massive book: {b.Name} ");
                        break;
                    case Book b:
                        Console.WriteLine($"Book: {b.Name} ");
                        break;
                    case BundleItem bundle when bundle.Items.Count > 1:
                        Console.WriteLine($"Bundle: {bundle.Name} with {bundle.Items.Count} items");
                        break;
                    default:
                        Console.WriteLine($"Other type: {item.Name}");
                        break;
                }
            }

            foreach (var item in repository)
            {
                if (item is Book { Author: "Uncle Bob" } book)
                {
                    Console.WriteLine($"Found Uncle's Bob book:{book}");
                }
            }
            
            
            ddd.AddTag(MediaTags.Discounted);
            if (ddd.HasTag(MediaTags.Discounted))
            {
                Console.WriteLine($"{ddd.Name} is discounted!");
            }
            
            Console.WriteLine(hobbit!.ToString("F", null));
            Console.WriteLine(ddd.ToString("S", null));
            
            var clonedBook = (Book) ddd.Clone();
            Console.WriteLine($"Cloned book has name {clonedBook.Name} and id {clonedBook.Id}");


            var (id, title, price) = ddd;
            Console.WriteLine($"Deconstruction: id={id}, title= {title}, price= {price}");
            
            
            var (titleOnly, author, pages) = ddd;
            Console.WriteLine($"Extension deconstruction: {titleOnly} by {author} with {pages} pages");
            
            
            var array = items.ToArray();
            var selectedRanfe = array.Length > 2 ? array[0..2] : array[..];
            
            Console.WriteLine("First elements (Range):");
            foreach (var item in selectedRanfe)
            {
                Console.WriteLine(item);
            }

            List<MediaItem>? reccomended = null;
            reccomended ??= new List<MediaItem>();
            foreach (var item in expensiveItems)
            {
                reccomended?.Add(item);
            }
            
            MediaItem[]? reccomendedArray = reccomended?.ToArray();
            var firstReccomended = reccomendedArray?[0];
            Console.WriteLine(firstReccomended?.ToString() ?? "No reccomended items");

            PrintItems(repository, header: "All items");
            
            items.Sort();
            Console.WriteLine("Sorted items by price:");
            PrintItems(repository);
            
            var discounted = repository.WithTag(MediaTags.Discounted).ToList();
            Console.WriteLine($"Discounted count: {discounted.Count}");

            Console.WriteLine("The end, have a nice day!  ");




        }

        private static void PrintItems(MediaRepository<MediaItem> repository, string header = "Goods")
        {
            Console.WriteLine($"{header}:");
            foreach (var item in repository)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        
    }
}
























