namespace DifferenceTwoListLib
{
    using System.ComponentModel;

    /// <summary>
    /// Art der möglichen Differenzen
    /// </summary>
    public enum DifferenceItemType
    {
        None = 0,
        [Description("Neuer Eintrag")]
        Add =1,
        [Description("Entfernter Eintrag")]
        Remove = 2,
        [Description("Geänderter Eintrag")]
        Change = 3
    }
}
