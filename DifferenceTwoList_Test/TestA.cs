/*
 * <copyright file="TestA.cs" company="Lifeprojects.de">
 *     Class: TestA
 *     Copyright © Lifeprojects.de 2022
 * </copyright>
 *
 * <author>Gerhard Ahrens - Lifeprojects.de</author>
 * <email>gerhard.ahrens@lifeprojects.de</email>
 * <date>06.10.2022 17:26:27</date>
 * <Project>DifferenceTwoList</Project>
 *
 * <summary>
 * Beschreibung zur Klasse
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

namespace DifferenceTwoList_Test
{
    using DifferenceTwoListLib;

    using System;
    using System.Diagnostics;

    [DebuggerDisplay("Id{this.Id};Status={this.Fullname}")]
    public class TestA : ISyncItem
    {
        public TestA()
        {
        }

        public Guid Id { get; set; }

        public string Fullname { get { return $"{this.MyProperty_A}|{this.MyProperty_B}|{this.MyProperty_C}|{this.MyProperty_D}"; } }

        public string Hash { get { return this.Fullname.ToMD5(); } }

        public int MyProperty_A { get; set; }

        public string MyProperty_B { get; set; }

        public DateTime MyProperty_C { get; set; }

        public ControlTyp MyProperty_D { get; set; } = ControlTyp.None;
    }
}
