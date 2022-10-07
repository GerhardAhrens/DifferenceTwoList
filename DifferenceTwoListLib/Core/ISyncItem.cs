namespace DifferenceTwoListLib
{
    using System;

    public interface ISyncItem
    {
        /// <summary>
        /// Id des Item
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Ermittelter MD5-Hash
        /// </summary>
        string Hash { get;}

        /// <summary>
        /// Ersteller Name aus ausgewählten Properties
        /// </summary>
        string Fullname { get; }
    }
}
