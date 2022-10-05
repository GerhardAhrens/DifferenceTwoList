//-----------------------------------------------------------------------
// <copyright file="UnitTestClass1.cs" company="www.pta.de">
//     Class: UnitTestClass1
//     Copyright © www.pta.de 2022
// </copyright>
//
// <author>Gerhard Ahrens - www.pta.de</author>
// <email>gerhard.ahrens@pta.de</email>
// <date>05.10.2022 14:43:48</date>
//
// <summary>
// Klasse für 
// </summary>
//-----------------------------------------------------------------------

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
    }
}
