namespace DifferenceTwoListLib
{
    using System.Collections.Generic;

    public class DataItemComparer<TCollection> : IEqualityComparer<TCollection>
    {
        public bool Equals(TCollection x, TCollection y)
        {
            return (string.Equals(((ISyncItem)x).Hash, ((ISyncItem)y).Hash));
        }

        public int GetHashCode(TCollection obj)
        {
            return ((ISyncItem)obj).Id.GetHashCode();
        }
    }
}
