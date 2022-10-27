/*
 * <copyright file="HashExtensions.cs" company="Lifeprojects.de">
 *     Class: HashExtensions
 *     Copyright © Lifeprojects.de 2022
 * </copyright>
 *
 * <author>Gerhard Ahrens - Lifeprojects.de</author>
 * <email>gerhard.ahrens@lifeprojects.de</email>
 * <date>07.10.2022</date>
 * <Project>DifferenceTwoListLib</Project>
 *
 * <summary>
 * Extension Klasse für den tp String
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

namespace System
{
    using System.Security.Cryptography;
    using System.Text;

    public static class HashExtensions
    {
        /// <summary>
        /// Gibt den Hash eines String als MD5-Hash zurück
        /// </summary>
        /// <param name="this">String von der MD5 Hash ermittelt werden soll</param>
        /// <param name="isUpperOrLower">True, MD5-Hash wird in Großbuchstanben zurückgegeben</param>
        /// <returns>MD5-Hash</returns>
        public static string ToMD5(this string @this, bool isUpperOrLower = false)
        {
            byte[] bytes = (new MD5CryptoServiceProvider()).ComputeHash(Encoding.UTF8.GetBytes(@this));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                if (isUpperOrLower == false)
                {
                    sb.Append(b.ToString("x2").ToLower());
                }
                else
                {
                    sb.Append(b.ToString("x2").ToUpper());
                }
            }

            return sb.ToString();
        }
    }
}
