/*
 * <copyright file="DifferenceCollection_Test.cs" company="Lifeprojects.de">
 *     Class: DifferenceCollection_Test
 *     Copyright © Lifeprojects.de 2022
 * </copyright>
 *
 * <author>Gerhard Ahrens - Lifeprojects.de</author>
 * <email>developer@lifeprojects.de</email>
 * <date>05.10.2022</date>
 * <Project>DifferenceTwoListLib</Project>
 *
 * <summary>
 * Abstrakte Klasse zur Erstellung einer ViewModel Klasse 
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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using DifferenceTwoListLib;

    using Microsoft.VisualStudio.TestTools.UnitTesting;


    [TestClass]
    public class DifferenceCollection_Test
    {
        [TestInitialize]
        public void Initialize()
        {
            CultureInfo culture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DifferenceCollection_Test"/> class.
        /// </summary>
        public DifferenceCollection_Test()
        {
        }

        [TestMethod]
        public void MainCollectionIsNull()
        {
            try
            {
                IEnumerable<DifferenceResultItem> diffCollection = DifferenceCollection.Result<DataItem>(null, null);
                Assert.Fail("An exception should have been thrown");
            }
            catch (NullReferenceException ex)
            {
                Assert.AreEqual("Die Collection 'mainCollection' darf nicht null sein.", ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Unexpected exception of type {ex.GetType()} caught: {ex.Message}");
            }
        }

        [TestMethod]
        public void SecondCollectionIsNull()
        {
            IEnumerable<DataItem> mainCollection = this.CreateMainData();

            IEnumerable<DifferenceResultItem> diffCollection = DifferenceCollection.Result<DataItem>(mainCollection, null);
            Assert.IsFalse(diffCollection == null);
            Assert.IsTrue(diffCollection.Count() == 6);
        }

        [TestMethod]
        public void ResultDifference()
        {
            IEnumerable<DataItem> mainCollection = this.CreateMainData();
            IEnumerable<DataItem> secondCollection = this.CreateSecondData();

            IEnumerable<DifferenceResultItem> diffCollection = DifferenceCollection.Result<DataItem>(mainCollection, secondCollection);

            Assert.IsFalse(diffCollection == null);
            Assert.IsTrue(diffCollection.Count() == 5);
            Assert.IsTrue(diffCollection.Count(c => c.DiffType == DifferenceItemType.Add) == 1);
            Assert.IsTrue(diffCollection.Count(c => c.DiffType == DifferenceItemType.Remove) == 2);
            Assert.IsTrue(diffCollection.Count(c => c.DiffType == DifferenceItemType.Change) == 2);
        }

        [TestMethod]
        public void ResultDifferenceV2()
        {
            IEnumerable<TestA> mainCollection = this.CreateMainDataV2();
            IEnumerable<TestA> secondCollection = this.CreateSecondDataV2();

            IEnumerable<DifferenceResultItem> diffCollection = DifferenceCollection.Result<TestA>(mainCollection, secondCollection);
            Assert.IsFalse(diffCollection == null);
            Assert.IsTrue(diffCollection.Count() == 4);
            Assert.IsTrue(diffCollection.Count(c => c.DiffType == DifferenceItemType.Add) == 1);
            Assert.IsTrue(diffCollection.Count(c => c.DiffType == DifferenceItemType.Remove) == 2);
            Assert.IsTrue(diffCollection.Count(c => c.DiffType == DifferenceItemType.Change) == 1);
        }

        [DataRow("", "")]
        [TestMethod]
        public void DataRowInputTest(string input, string expected)
        {
        }

        [TestMethod]
        public void ExceptionTest()
        {
            try
            {
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(Exception));
            }
        }

        private IEnumerable<DataItem> CreateMainData()
        {
            List<DataItem> mainCollection = new List<DataItem>();
            mainCollection.Add(new DataItem() { Id = new Guid("{006DED8D-266E-4F55-BA7B-3CD0E1502CF5}"), Data = "Pferd", Value = "0" });
            mainCollection.Add(new DataItem() { Id = new Guid("{283EFD5E-059D-48EA-821C-A290A2739CEF}"), Data = "Hund", Value = "1" });
            mainCollection.Add(new DataItem() { Id = new Guid("{102B71EB-BAC4-4673-AF5A-FCD6F454275C}"), Data = "Katze", Value = "3" });
            mainCollection.Add(new DataItem() { Id = new Guid("{D5471D9F-C767-4EC8-A31D-ABFB07D65EA3}"), Data = "Maus", Value = "4" });
            mainCollection.Add(new DataItem() { Id = new Guid("{E4AEA1FB-DE37-448A-9E5F-8591D1B3D175}"), Data = "Bär", Value = "0" });
            mainCollection.Add(new DataItem() { Id = new Guid("{F466B9FB-42B4-4CB3-8F40-3E1527A6AE72}"), Data = "Tiger", Value = "3" });

            return mainCollection;
        }

        private IEnumerable<DataItem> CreateSecondData()
        {
            List<DataItem> secondCollection = new List<DataItem>();
            secondCollection.Add(new DataItem() { Id = new Guid("{006DED8D-266E-4F55-BA7B-3CD0E1502CF5}"), Data = "Pferd", Value = "0" });
            secondCollection.Add(new DataItem() { Id = new Guid("{283EFD5E-059D-48EA-821C-A290A2739CEF}"), Data = "Hund", Value = "2" });
            secondCollection.Add(new DataItem() { Id = new Guid("{D5471D9F-C767-4EC8-A31D-ABFB07D65EA3}"), Data = "Maus", Value = "4" });
            secondCollection.Add(new DataItem() { Id = new Guid("{8CC57D56-5210-4A4E-B514-045FA0969A97}"), Data = "Esel", Value = "9" });
            secondCollection.Add(new DataItem() { Id = new Guid("{E4AEA1FB-DE37-448A-9E5F-8591D1B3D175}"), Data = "Bär", Value = "3" });

            return secondCollection;
        }

        private IEnumerable<TestA> CreateMainDataV2()
        {
            List<TestA> mainCollection = new List<TestA>();
            TestA mainItem = new TestA();
            mainItem.Id = new Guid("{6157AA58-B85D-49D5-95B0-ED470520FE0F}");
            mainItem.MyProperty_A = 10;
            mainItem.MyProperty_B = "gerhard_10";
            mainItem.MyProperty_C = new DateTime(1960, 6, 28);
            mainItem.MyProperty_D = ControlTyp.PinTxt;
            mainCollection.Add(mainItem);
            mainItem = new TestA();
            mainItem.Id = new Guid("{97391406-248D-490D-82CA-12B7F51AF6CE}");
            mainItem.MyProperty_A = 12;
            mainItem.MyProperty_B = "gerhard_12";
            mainItem.MyProperty_C = new DateTime(1960, 10, 29);
            mainItem.MyProperty_D = ControlTyp.WebsiteTxt;
            mainCollection.Add(mainItem);
            mainItem = new TestA();
            mainItem.Id = new Guid("{22E25C08-B671-407B-ADA1-5BF745329F66}");
            mainItem.MyProperty_A = 13;
            mainItem.MyProperty_B = "gerhard_13";
            mainItem.MyProperty_C = new DateTime(1960, 10, 29);
            mainItem.MyProperty_D = ControlTyp.WebsiteTxt;
            mainCollection.Add(mainItem);

            return mainCollection;
        }

        private IEnumerable<TestA> CreateSecondDataV2()
        {
            List<TestA> secondCollection = new List<TestA>();
            TestA syncItem = new TestA();
            syncItem.Id = new Guid("{6157AA58-B85D-49D5-95B0-ED470520FE0F}");
            syncItem.MyProperty_A = 10;
            syncItem.MyProperty_B = "gerhard_10";
            syncItem.MyProperty_C = new DateTime(1960, 6, 28);
            syncItem.MyProperty_D = ControlTyp.WebsiteTxt;
            secondCollection.Add(syncItem);
            syncItem = new TestA();
            syncItem.Id = new Guid("{24B845E1-E05F-4DC2-8C09-2F3222683995}");
            syncItem.MyProperty_A = 11;
            syncItem.MyProperty_B = "gerhard_11";
            syncItem.MyProperty_C = new DateTime(1960, 6, 28);
            syncItem.MyProperty_D = ControlTyp.UsernameTxt;
            secondCollection.Add(syncItem);


            return secondCollection;
        }
    }
}
