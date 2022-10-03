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
            secondCollection.Add(new DataItem() {Id = new Guid("{8CC57D56-5210-4A4E-B514-045FA0969A97}"), Data = "Esel", Value = "9" });
            secondCollection.Add(new DataItem() {Id = new Guid("{E4AEA1FB-DE37-448A-9E5F-8591D1B3D175}"), Data = "Bär", Value = "3" });

            List<DiffItem> diffList = GetDifferenceResult(mainCollection, secondCollection);
        }

        private static List<DiffItem> GetDifferenceResult(IEnumerable<DataItem> preList, IEnumerable<DataItem> postList)
        {
            List<DiffItem>  resultCollection = new List<DiffItem>();

            IEnumerable<DataItem> preOnly = preList.Except(postList, new DataItemComparer());
            IEnumerable<DataItem> postOnly = postList.Except(preList, new DataItemComparer());
            IEnumerable<DataItem> common = postList.Intersect(preList, new DataItemComparer());

            IEnumerable<DataItem> added = postOnly.Except(preOnly, new DataItemDataComparer());
            IEnumerable<DataItem> removed = preOnly.Except(postOnly, new DataItemDataComparer());
            IEnumerable<DataItem> diffPre = preOnly.Intersect(postOnly, new DataItemDataComparer());
            IEnumerable<DataItem> diffPost = postOnly.Intersect(preOnly, new DataItemDataComparer());

            foreach (DataItem add in added)
            {
                resultCollection.Add(new DiffItem(add.Id, add.Data, DiffType.Add, null, add.Value));
            }
            foreach (DataItem rem in removed)
            {
                resultCollection.Add(new DiffItem(rem.Id, rem.Data, DiffType.Remove, rem.Value, null));
            }
            foreach (DataItem pre in diffPre)
            {
                DataItem post = diffPost.First(x => x.Data == pre.Data);
                resultCollection.Add(new DiffItem(pre.Id, pre.Data, DiffType.Diff, pre.Value, post.Value));
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

        public DiffItem(Guid id, string data, DiffType type, string pre, string post)
        {
            this.Id = id;
            this.Data = data;
            this.DiffType = type;
            this.PreVal = pre;
            this.PostVal = post; 
        }

        public Guid Id { get; private set; }

        public string Data { get; set; }

        public DiffType DiffType { get; set; } // DiffType = Add/Remove/Diff

        public string PreVal { get; set; } // preList value corresponding to Data item

        public string PostVal { get; set; } // postList value corresponding to Data item
    }

    public class DataItemComparer : IEqualityComparer<DataItem>
    {
        public bool Equals(DataItem x, DataItem y)
        {
            return (string.Equals(x.Hash, y.Hash));
        }

        public int GetHashCode(DataItem obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public class DataItemDataComparer : IEqualityComparer<DataItem>
    {
        public bool Equals(DataItem x, DataItem y)
        {
            return string.Equals(x.Id, y.Id);
        }

        public int GetHashCode(DataItem obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public enum DiffType
    {
        Add,
        Remove,
        Diff
    }
}
