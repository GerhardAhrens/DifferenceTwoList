namespace DifferenceTwoListLib
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Die Klasse ermittelt zwischen 2 Collection die Unterschiede und gibt diese als Liste zurück.
    /// </summary>
    public static class DifferenceCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DifferenceFromCollection"/> class.
        /// </summary>
        static DifferenceCollection()
        {
        }

        /// <summary>
        /// Unterschiede zwischen zwei Collektion ermitteln
        /// </summary>
        /// <typeparam name="TCollection">Typ der übergeben Collections</typeparam>
        /// <param name="mainCollection">Main Collection</param>
        /// <param name="secondCollection">Second Collection</param>
        /// <returns>Liste mit Unterschieden, ist die zweite Liste leer oder null, so werden alle Einträge als neue Einträge der 'Main Collection' zurückgegeben.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static List<DifferenceResultItem> Result<TCollection>(IEnumerable<TCollection> mainCollection, IEnumerable<TCollection> secondCollection)
        {
            if (mainCollection == null)
            {
                throw new NullReferenceException($"Die Collection '{nameof(mainCollection)}' darf nicht null sein.");
            }

            List<DifferenceResultItem> resultCollection = new List<DifferenceResultItem>();

            if (secondCollection == null || secondCollection.Count() == 0)
            {
                if (mainCollection != null || mainCollection.Count() != 0)
                {
                    foreach (TCollection addItem in mainCollection)
                    {
                        resultCollection.Add(new DifferenceResultItem(((ISyncItem)addItem).Id, ((ISyncItem)addItem).Fullname, DifferenceItemType.Add));
                    }

                    return resultCollection;
                }
                else
                {
                    return resultCollection;
                }
            }

            IEnumerable<TCollection> mainCollectionOnly = mainCollection.Except(secondCollection, new CollectionItemComparer<TCollection>());
            IEnumerable<TCollection> secondCollectionOnly = secondCollection.Except(mainCollection, new CollectionItemComparer<TCollection>());
            IEnumerable<TCollection> common = secondCollection.Intersect(mainCollection, new CollectionItemComparer<TCollection>());

            IEnumerable<TCollection> addedItems = secondCollectionOnly.Except(mainCollectionOnly, new CollectionItemIdComparer<TCollection>());
            IEnumerable<TCollection> removedItems = mainCollectionOnly.Except(secondCollectionOnly, new CollectionItemIdComparer<TCollection>());
            IEnumerable<TCollection> diffMain = mainCollectionOnly.Intersect(secondCollectionOnly, new CollectionItemIdComparer<TCollection>());
            IEnumerable<TCollection> diffSecond = secondCollectionOnly.Intersect(mainCollectionOnly, new CollectionItemIdComparer<TCollection>());

            foreach (TCollection add in addedItems)
            {
                resultCollection.Add(new DifferenceResultItem(((ISyncItem)add).Id, ((ISyncItem)add).Fullname, DifferenceItemType.Add));
            }
            foreach (TCollection rem in removedItems)
            {
                resultCollection.Add(new DifferenceResultItem(((ISyncItem)rem).Id, ((ISyncItem)rem).Fullname, DifferenceItemType.Remove));
            }
            foreach (TCollection pre in diffMain)
            {
                TCollection post = diffSecond.First(x => ((ISyncItem)x).Id == ((ISyncItem)pre).Id);
                resultCollection.Add(new DifferenceResultItem(((ISyncItem)pre).Id, ((ISyncItem)pre).Fullname, DifferenceItemType.Change));
            }

            return resultCollection;
        }
    }
}
