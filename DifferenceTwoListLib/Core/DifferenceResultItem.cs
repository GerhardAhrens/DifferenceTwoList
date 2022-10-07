namespace DifferenceTwoListLib
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Die Klasse gibt als Typ einer Collection die ermittelte Unterschiede zurück
    /// </summary>
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

        /// <summary>
        /// Id des Item bei dem die Different ermittelt wurde
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Daten, z.B. verschiedene Properties als String
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Art der Differenz, Add, Remove, Change
        /// </summary>
        public DifferenceItemType DiffType { get; set; }
    }
}
