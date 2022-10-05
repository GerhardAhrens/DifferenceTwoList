namespace DifferenceTwoListLib
{
    using System.Collections.Generic;

    public class CollectionItemIdComparer<TCollection> : IEqualityComparer<TCollection>
    {
        public bool Equals(TCollection x, TCollection y)
        {
            return string.Equals(((ISyncItem)x).Id, ((ISyncItem)y).Id);
        }

        public int GetHashCode(TCollection obj)
        {
            return ((ISyncItem)obj).Id.GetHashCode();
        }
    }
}
