namespace DifferenceTwoList
{
    using System;
    using System.Diagnostics;

    using DifferenceTwoListLib;

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

        public string Hash { get { return this.Fullname.ToMD5(); } }

        public string Data { get; set; }

        public string Value { get; set; }
    }
}
