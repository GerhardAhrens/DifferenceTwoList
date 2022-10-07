/*
 * <copyright file="ISyncItem.cs" company="Lifeprojects.de">
 *     Class: ISyncItem
 *     Copyright © Lifeprojects.de 2022
 * </copyright>
 *
 * <author>Gerhard Ahrens - Lifeprojects.de</author>
 * <email>gerhard.ahrens@lifeprojects.de</email>
 * <date>07.10.2022</date>
 * <Project>DifferenceTwoListLib</Project>
 *
 * <summary>
 * Die Interface Klasse zur Implementierung für Collection Typen 
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

    public interface ISyncItem
    {
        /// <summary>
        /// Id des Item
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Ermittelter MD5-Hash
        /// </summary>
        string Hash { get;}

        /// <summary>
        /// Ersteller Name aus ausgewählten Properties
        /// </summary>
        string Fullname { get; }
    }
}
