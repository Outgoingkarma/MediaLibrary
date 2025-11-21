

using System.Linq.Expressions;
using MediaLibrary.Core;

namespace MediaLibrary.App
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var items = new List<MediaItem>();

            if (MediaFactory.TryCreateBook("The Hobbit", "", "123", out var hobbit))
            {
                items.Add(hobbit);
            }


            var ddd = new Book(
                name: "Clean code",
                author: "Uncle bob",
                pages: 216,
                price: 39.88m,
                tags: MediaTags.New | MediaTags.Popular);
            items.Add(ddd);
            
            
            var bundled = new BundleItem("Starter pack", 39.99m, hobbit, ddd);
            items.Add(bundled);
            
            Predicate<MediaItem> expensivePredicate = item => item.Price > 30m;
            var expensiveItems = items.FindAll(expensivePredicate);


            foreach (var item in items)
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

            foreach (var item in items)
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
            
            Console.WriteLine(hobbit.ToString("F", null));
            Console.WriteLine(ddd.ToString("S", null));


            var (id, title, price) = ddd;
            Console.WriteLine($"Deconstruction: id={id}, title= {title}, price= {price}");
            
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

            PrintItems(items, header: "All items");
            
            items.Sort();
            Console.WriteLine("Sorted items by price:");
            PrintItems(items);
            
            Console.WriteLine("The end, have a nice day!  ");




        }

        private static void PrintItems(IReadOnlyCollection<MediaItem> items, string header = "Goods")
        {
            Console.WriteLine($"{header}:");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        
    }
}
























