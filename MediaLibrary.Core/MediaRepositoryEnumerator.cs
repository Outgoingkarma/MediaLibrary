using System.Collections;

namespace MediaLibrary.Core
{
    public class MediaRepositoryEnumerator<T> : IEnumerator<T>
    {
        private readonly IList<T> _items;
        private int _index = -1;

        public MediaRepositoryEnumerator(IList<T> items)
        {
            _items = items ?? throw new InvalidMediaException("Enumerator cannot iterate ovver a null collection.");
        }

        public T Current
        {
            get
            {
                if (_index < 0 || _index >= _items.Count)
                {
                    throw new InvalidMediaException("Enumerator is not positioned on a valid element.");
                }
                return _items[_index];
            }
        }
        object IEnumerator.Current => Current!;

        public bool MoveNext()
        {
            if (_index + 1 >= _items.Count) return false;
            _index++;
            return true;
        }

        public void Reset()
        {
            _index = -1;
        } 
        public void Dispose() { }
    }
}