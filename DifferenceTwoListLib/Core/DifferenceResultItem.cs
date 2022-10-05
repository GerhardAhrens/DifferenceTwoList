namespace DifferenceTwoListLib
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("Data={this.Data};DiffType={this.DiffType}")]
    public sealed class DifferenceResultItem
    {
        public DifferenceResultItem() { }

        public DifferenceResultItem(Guid id, string fullname, DifferenceItemType type)
        {
            this.Id = id;
            this.Data = fullname;
            this.DiffType = type;
        }

        public Guid Id { get; private set; }

        public string Data { get; set; }

        public DifferenceItemType DiffType { get; set; }
    }
}
