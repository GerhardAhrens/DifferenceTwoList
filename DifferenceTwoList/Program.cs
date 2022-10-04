//https://codereview.stackexchange.com/questions/169516/comparing-two-lists-of-class-objects-similar-to-a-diff-tool

namespace DifferenceTwoList
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

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

            List<DiffItem> diffList = GetDifferenceResult(mainCollection, secondCollection);
        }

        private static List<DiffItem> GetDifferenceResult(IEnumerable<DataItem> mainCollection, IEnumerable<DataItem> secondCollection)
        {
            if (mainCollection == null)
            {
                throw new NullReferenceException($"Die Collection '{nameof(mainCollection)}' darf nicht null sein.");
            }

            List<DiffItem>  resultCollection = new List<DiffItem>();

            if (secondCollection == null || secondCollection.Count() == 0)
            {
                if (mainCollection != null || mainCollection.Count() != 0)
                {
                    foreach (DataItem addItem in mainCollection)
                    {
                        resultCollection.Add(new DiffItem(addItem.Id, addItem.Fullname, DiffType.Add));
                    }

                    return resultCollection;
                }
                else
                {
                    return resultCollection;
                }
            }

            IEnumerable<DataItem> mainCollectionOnly = mainCollection.Except(secondCollection, new DataItemComparer<DataItem>());
            IEnumerable<DataItem> secondCollectionOnly = secondCollection.Except(mainCollection, new DataItemComparer<DataItem>());
            IEnumerable<DataItem> common = secondCollection.Intersect(mainCollection, new DataItemComparer<DataItem>());

            IEnumerable<DataItem> addedItems = secondCollectionOnly.Except(mainCollectionOnly, new DataItemDataComparer<DataItem>());
            IEnumerable<DataItem> removedItems = mainCollectionOnly.Except(secondCollectionOnly, new DataItemDataComparer<DataItem>());
            IEnumerable<DataItem> diffMain = mainCollectionOnly.Intersect(secondCollectionOnly, new DataItemDataComparer<DataItem>());
            IEnumerable<DataItem> diffSecond = secondCollectionOnly.Intersect(mainCollectionOnly, new DataItemDataComparer<DataItem>());

            foreach (DataItem add in addedItems)
            {
                resultCollection.Add(new DiffItem(add.Id, add.Data, DiffType.Add));
            }
            foreach (DataItem rem in removedItems)
            {
                resultCollection.Add(new DiffItem(rem.Id, rem.Data, DiffType.Remove));
            }
            foreach (DataItem pre in diffMain)
            {
                DataItem post = diffSecond.First(x => x.Id == pre.Id);
                resultCollection.Add(new DiffItem(pre.Id, pre.Data, DiffType.Diff));
            }

            return resultCollection;
        }
    }

    public interface ISyncItem
    {
        Guid Id { get; set; }

        string Hash { get;}

        string Fullname { get; }
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

    [DebuggerDisplay("Data={this.Data};DiffType={this.DiffType}")]
    public class DiffItem
    {
        public DiffItem() { }

        public DiffItem(Guid id, string fullname, DiffType type)
        {
            this.Id = id;
            this.Data = fullname;
            this.DiffType = type;
        }

        public Guid Id { get; private set; }

        public string Data { get; set; }

        public DiffType DiffType { get; set; } // DiffType = Add/Remove/Diff
    }

    public class DataItemComparer<TCollection> : IEqualityComparer<TCollection>
    {
        public bool Equals(TCollection x, TCollection y)
        {
            return (string.Equals(((ISyncItem)x).Hash, ((ISyncItem)y).Hash));
        }

        public int GetHashCode(TCollection obj)
        {
            return ((ISyncItem)obj).Id.GetHashCode();
        }
    }

    public class DataItemDataComparer<TCollection> : IEqualityComparer<TCollection>
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

    public enum DiffType
    {
        Add,
        Remove,
        Diff
    }
}
