using System;

namespace MediaLibrary.Core
{
    [Flags]
    public enum MediaTags
    {
        None = 0,
        New = 1,
        Popular = 2,
        Discounted = 4,
        Archived = 8
    }
}