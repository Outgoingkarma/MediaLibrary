using System;
namespace MediaLibrary.Core
{
    public abstract class MediaItem :
        IIdentifiable,
        IComparable<MediaItem>,
        IEquatable<MediaItem>,
        IFormattable
    {
        private static int _nextId;
        static MediaItem()
        {
            _nextId = 1;
        }

        protected MediaItem(string name, decimal price, MediaTags tags = MediaTags.None)
        {
            Id = _nextId++;
            Name = name;
            Price = price;
            Tags = tags;
        }
        public int Id { get; }
        public string Name { get; }
        public decimal Price { get; protected set;}
        public MediaTags Tags { get; protected set;}
        
        public void AddTag(MediaTags tag) => Tags |= tag;
        public void RemoveTag(MediaTags tag) => Tags &= ~tag;
        public bool HasTag(MediaTags tag) => (Tags & tag) != 0;

        public int CompareTo(MediaItem? other)
        {
            if (other is null) return 1;
            var priceComparison = Price.CompareTo(other.Price);
            if (priceComparison !=0)
                return priceComparison;
            return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(MediaItem? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return Id == other.Id;
        }
        
        public override bool Equals(object? obj) => Equals(obj as MediaItem);
        public override int GetHashCode() => Id.GetHashCode();
        
        public static bool operator ==(MediaItem? left, MediaItem? right) => Equals(left, right);

        public static bool operator !=(MediaItem? left, MediaItem? right) => !Equals(left, right);


        public static MediaItem operator +(MediaItem left, MediaItem right)
        {
            if (left is null) throw new ArgumentNullException(nameof(left));
            if (right is null) throw new ArgumentNullException(nameof(right));
            return new BundleItem(
                name: $"{left.Name} + {right.Name}",
                price: left.Price + right.Price,
                left, right);
        }
        
        
        public override string ToString() => ToString("G", null);

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            format ??= "G";
            return format switch
            {
                "G" => $"{Name} ({Price}$)",
                "F" => $"{Id}: {Name}, price: {Price}$, tags: {Tags}",
                "S" => Name,
                _ => Name
            };
        }

        public void Deconstruct(out int id, out string name, out decimal price)
        {
            id = Id;
            name = Name;
            price = Price;
        }

        
    }
}



