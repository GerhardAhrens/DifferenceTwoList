/*
 * <copyright file="DifferenceCollection.cs" company="Lifeprojects.de">
 *     Class: DifferenceCollection
 *     Copyright © Lifeprojects.de 2022
 * </copyright>
 *
 * <author>Gerhard Ahrens - Lifeprojects.de</author>
 * <email>developer@lifeprojects.de</email>
 * <date>05.10.2022</date>
 * <Project>DifferenceTwoListLib</Project>
 *
 * <summary>
 * Klasse zur Ermittlung von Difference zwischen zwei Collection
 * </summary>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by the Free Software Foundation, 
 * either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful,but WITHOUT ANY WARRANTY; 
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.You should have received a copy of the GNU General Public License along with this program. 
 * If not, see <http://www.gnu.org/licenses/>.
*/

namespace DifferenceTwoListLib
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class DifferenceCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DifferenceFromCollection"/> class.
        /// </summary>
        static DifferenceCollection()
        {
        }

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
