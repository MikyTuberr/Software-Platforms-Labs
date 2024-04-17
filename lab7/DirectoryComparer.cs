using System.Collections;

namespace PT_7
{
    [Serializable]
    internal class DirectoryComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            if (x == null && y == null)
                return 0;
            else if (x == null)
                return -1;
            else if (y == null)
                return 1;

            int lengthComparison = x.Length.CompareTo(y.Length);
            if (lengthComparison != 0)
                return lengthComparison;

            return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
        }
    }

}