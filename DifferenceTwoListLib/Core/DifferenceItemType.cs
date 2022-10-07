/*
 * <copyright file="DifferenceItemType.cs" company="Lifeprojects.de">
 *     Class: DifferenceItemType
 *     Copyright © Lifeprojects.de 2022
 * </copyright>
 *
 * <author>Gerhard Ahrens - Lifeprojects.de</author>
 * <email>gerhard.ahrens@lifeprojects.de</email>
 * <date>07.10.2022</date>
 * <Project>DifferenceTwoListLib</Project>
 *
 * <summary>
 * Enum Klasse 
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
    using System.ComponentModel;

    /// <summary>
    /// Art der möglichen Differenzen
    /// </summary>
    public enum DifferenceItemType
    {
        None = 0,
        [Description("Neuer Eintrag")]
        Add =1,
        [Description("Entfernter Eintrag")]
        Remove = 2,
        [Description("Geänderter Eintrag")]
        Change = 3
    }
}
