/*
 * <copyright file="CollectionItemIdComparer.cs" company="Lifeprojects.de">
 *     Class: CollectionItemIdComparer
 *     Copyright © Lifeprojects.de 2022
 * </copyright>
 *
 * <author>Gerhard Ahrens - Lifeprojects.de</author>
 * <email>gerhard.ahrens@lifeprojects.de</email>
 * <date>07.10.2022</date>
 * <Project>DifferenceTwoListLib</Project>
 *
 * <summary>
 * Comparer Klasse zum vergleichen von Id (Guid)
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
