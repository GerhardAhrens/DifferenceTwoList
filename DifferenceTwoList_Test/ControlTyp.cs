//-----------------------------------------------------------------------
// <copyright file="ControlTyp.cs" company="Lifeprojects.de">
//     Class: ControlTyp
//     Copyright � Lifeprojects.de 2022
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>gerhard.ahrens@lifeprojects.de</email>
// <date>24.04.2022</date>
//
// <summary>
// Enum Klasse f�r Dialog Controls (Ein- oder Ausblenden)
// </summary>
//-----------------------------------------------------------------------

namespace DifferenceTwoList_Test
{
    using System;

    public enum ControlTyp : int
    {
        None = 0,
        WebsiteTxt = 1,
        PinTxt = 2,
        UsernameTxt = 3,
        PasswortTxt = 4,
    }
}
