namespace DifferenceTwoListLib
{
    using System;

    public interface ISyncItem
    {
        Guid Id { get; set; }

        string Hash { get;}

        string Fullname { get; }
    }
}
