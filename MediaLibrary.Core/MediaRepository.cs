
using System.Collections;

namespace MediaLibrary.Core
{
    public class MediaRepository<T> : IEnumerable<T> where T : MediaItem
    {
        private readonly List<T> _items = new();
        public event EventHandler<T>? ItemAdded;

        public void Add(T item)
        {
            if(item is null) 
            {
                throw new InvalidMediaException("Cannot add a null media item.");
            }
            if (item.Price <= 0)
            {
                throw new InvalidMediaException($"Item {item.Name} has an invalid negative price.");
            }

            if (_items.Any(existing => existing.Id == item.Id))
            {
                throw new InvalidMediaException($"Item with id {item.Id} already exists in the repository.");
            }
            
            _items.Add(item);
            ItemAdded?.Invoke(this, item);
        }
        public IReadOnlyCollection<T> Items => _items.AsReadOnly();
        public T this[int index] => _items[index];
        public IEnumerator<T> GetEnumerator()
        {
            return new MediaRepositoryEnumerator<T>(_items);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}