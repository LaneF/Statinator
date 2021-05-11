// (c) Copyright Cleverous 2017. All rights reserved.

namespace Cleverous.Stats
{
    // **     Here you can modify the possible Stats. This file is technically "Core", but you have to     **
    // **     modify it in order to add additional Stats, so feel free to do so.                           **
    // **                                                                                                  **
    // **     If you add stats only to the end of the Enum, you will not break example content.            **
    // **     If you heavily modify this, it will break all example content in Statinator and Deftly.      **

    /// <summary>
    /// <para>A list(enum) of possible Stat Types.</para>
    /// <para>Frequently used to quickly lookup a type of stat on a character.</para>
    /// </summary>
    public enum StatType { Health, Mana, Level, Experience, ExpReward, Agility, Dexterity, Endurance, Strength, RegenHp, RegenMp }
}