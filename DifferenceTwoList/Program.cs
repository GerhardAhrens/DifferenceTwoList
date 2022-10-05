namespace DifferenceTwoList
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using DifferenceTwoListLib;

    public class Program
    {
        private static void Main(string[] args)
        {
            List<DataItem> mainCollection = new List<DataItem>();
            mainCollection.Add(new DataItem() { Id = new Guid("{006DED8D-266E-4F55-BA7B-3CD0E1502CF5}"), Data = "Pferd", Value = "0" });
            mainCollection.Add(new DataItem() { Id = new Guid("{283EFD5E-059D-48EA-821C-A290A2739CEF}"), Data = "Hund", Value = "1" });
            mainCollection.Add(new DataItem() { Id = new Guid("{102B71EB-BAC4-4673-AF5A-FCD6F454275C}"), Data = "Katze", Value = "3" });
            mainCollection.Add(new DataItem() { Id = new Guid("{D5471D9F-C767-4EC8-A31D-ABFB07D65EA3}"), Data = "Maus", Value = "4" });
            mainCollection.Add(new DataItem() { Id = new Guid("{E4AEA1FB-DE37-448A-9E5F-8591D1B3D175}"), Data = "Bär", Value = "0" });
            mainCollection.Add(new DataItem() { Id = new Guid("{F466B9FB-42B4-4CB3-8F40-3E1527A6AE72}"), Data = "Tiger", Value = "3" });

            List<DataItem> secondCollection = new List<DataItem>();
            secondCollection.Add(new DataItem() { Id = new Guid("{006DED8D-266E-4F55-BA7B-3CD0E1502CF5}"), Data = "Pferd", Value = "0" });
            secondCollection.Add(new DataItem() { Id = new Guid("{283EFD5E-059D-48EA-821C-A290A2739CEF}"), Data = "Hund", Value = "2" });
            secondCollection.Add(new DataItem() { Id = new Guid("{D5471D9F-C767-4EC8-A31D-ABFB07D65EA3}"), Data = "Maus", Value = "4" });
            secondCollection.Add(new DataItem() { Id = new Guid("{8CC57D56-5210-4A4E-B514-045FA0969A97}"), Data = "Esel", Value = "9" });
            secondCollection.Add(new DataItem() { Id = new Guid("{E4AEA1FB-DE37-448A-9E5F-8591D1B3D175}"), Data = "Bär", Value = "3" });

            List<DifferenceResultItem> diffList = GetDifferenceResult<DataItem>(mainCollection, secondCollection);
        }

        private static List<DifferenceResultItem> GetDifferenceResult<TCollection>(IEnumerable<TCollection> mainCollection, IEnumerable<TCollection> secondCollection)
        {
            if (mainCollection == null)
            {
                throw new NullReferenceException($"Die Collection '{nameof(mainCollection)}' darf nicht null sein.");
            }

            List<DifferenceResultItem>  resultCollection = new List<DifferenceResultItem>();

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

            IEnumerable<TCollection> mainCollectionOnly = mainCollection.Except(secondCollection, new DataItemComparer<TCollection>());
            IEnumerable<TCollection> secondCollectionOnly = secondCollection.Except(mainCollection, new DataItemComparer<TCollection>());
            IEnumerable<TCollection> common = secondCollection.Intersect(mainCollection, new DataItemComparer<TCollection>());

            IEnumerable<TCollection> addedItems = secondCollectionOnly.Except(mainCollectionOnly, new DataItemDataComparer<TCollection>());
            IEnumerable<TCollection> removedItems = mainCollectionOnly.Except(secondCollectionOnly, new DataItemDataComparer<TCollection>());
            IEnumerable<TCollection> diffMain = mainCollectionOnly.Intersect(secondCollectionOnly, new DataItemDataComparer<TCollection>());
            IEnumerable<TCollection> diffSecond = secondCollectionOnly.Intersect(mainCollectionOnly, new DataItemDataComparer<TCollection>());

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
                resultCollection.Add(new DifferenceResultItem(((ISyncItem)pre).Id, ((ISyncItem)pre).Fullname, DifferenceItemType.Diff));
            }

            return resultCollection;
        }
    }

    [DebuggerDisplay("Data={this.Data};Value={this.Value}")]
    public class DataItem : ISyncItem
    {
        public DataItem()
        {
            this.Id = Guid.NewGuid();
        }

        public DataItem(Guid id, string d, string v)
        {
            this.Id = id;
            this.Data = d;
            this.Value = v; 
        }

        public Guid Id { get; set; }

        public string Fullname { get { return $"{this.Data}|{this.Value}"; } }

        public string Hash { get { return this.ToMD5(this.Fullname); } }

        public string Data { get; set; }

        public string Value { get; set; }

        private string ToMD5(string @this, bool isUpperOrLower = false)
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
