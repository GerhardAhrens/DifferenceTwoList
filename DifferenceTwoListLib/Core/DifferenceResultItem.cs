/*
 * <copyright file="DifferenceResultItem.cs" company="Lifeprojects.de">
 *     Class: DifferenceResultItem
 *     Copyright © Lifeprojects.de 2022
 * </copyright>
 *
 * <author>Gerhard Ahrens - Lifeprojects.de</author>
 * <email>developer@lifeprojects.de</email>
 * <date>05.10.2022</date>
 * <Project>DifferenceTwoListLib</Project>
 *
 * <summary>
 * Klasse für
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
    using System.Diagnostics;

    [DebuggerDisplay("Data={this.Data};DiffType={this.DiffType}")]
    public sealed class DifferenceResultItem
    {
        public DifferenceResultItem() { }

        public DifferenceResultItem(Guid id, string fullname, DifferenceItemType type)
        {
            this.Id = id;
            this.Data = fullname;
            this.DiffType = type;
        }

        public Guid Id { get; private set; }

        public string Data { get; set; }

        public DifferenceItemType DiffType { get; set; }
    }
}
