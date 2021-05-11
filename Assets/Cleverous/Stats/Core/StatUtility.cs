// (c) Copyright Cleverous 2017. All rights reserved.

using System;
using UnityEngine;

namespace Cleverous.Stats
{
	public static partial class StatUtility 
	{
	    public static Stat[] BaseCharacterStats()
	    {
            int statCount = Enum.GetNames(typeof(StatType)).Length;
            Stat[] s = new Stat[statCount];

            //                Stat                        Unregulated    Base     Min     Max       Aff.        MaxAff.    Actual  //
	        s[(int) StatType.Level]         = new Stat(     true,        0,       0,      20,       1,          0,         0        );
	        s[(int) StatType.Experience]    = new Stat(     true,        0,       0,      1000,     0,          1000,      0        );
	        s[(int) StatType.ExpReward]     = new Stat(     false,       450,     450,    6000,     0,          50,        450      );
	        s[(int) StatType.Health]        = new Stat(     true,        100,     0,      100,      0,          8,         9999     );
	        s[(int) StatType.Mana]          = new Stat(     true,        100,     0,      100,      0,          8,         9999     );
	        s[(int) StatType.Agility]       = new Stat(     false,       10,      1,      100,      0.1f,       0,         9999     );
	        s[(int) StatType.Dexterity]     = new Stat(     false,       10,      1,      100,      0.1f,       0,         9999     );
	        s[(int) StatType.Endurance]     = new Stat(     false,       10,      1,      100,      2,          0,         9999     );
	        s[(int) StatType.Strength]      = new Stat(     false,       12,      1,      100,      3,          0,         9999     );
	        s[(int) StatType.RegenHp]       = new Stat(     false,       0.5f,    0,      5,        0.25f,      0,         9999     );
	        s[(int) StatType.RegenMp]       = new Stat(     false,       0.5f,    0,      5,        0.25f,      0,         9999     );

	        return s;
	    }

        public static float GetRegulatedStatValue(Stat stat)
        {
            return
                Mathf.Clamp(stat.Get(StatProperty.Base)
                + stat.Owner.Stats[(int)StatType.Level].GetRoot(StatProperty.Value)
                * stat.Get(StatProperty.Affinity),
                stat.Get(StatProperty.Min),
                stat.Get(StatProperty.Max));
        }
    }
}