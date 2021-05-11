// (c) Copyright Cleverous 2017. All rights reserved.

using UnityEngine;

namespace Cleverous.Stats
{
	public static partial class StatUtility
	{
        /// <summary>
        /// Example of checking stats to see if something is below 20% health.
        /// </summary>
	    public static bool ExampleIsWeak(IUseStats target)
	    {
            return target.GetStatValue(StatType.Health) < target.GetStatMax(StatType.Health) / 4;
	    }
        /// <summary>
        /// Give the target character an amount of XP
        /// </summary>
        /// <param name="amount">How much XP</param>
        /// <param name="target">The target interface</param>
        public static void ExampleGiveXp(int amount, IUseStats target)
        {
            // Level Up the character if the input amount exceeds their next maximum.
            int val = (int)target.Stats[(int)StatType.Experience].Get(StatProperty.Value) + amount;
            if (val >= target.Stats[(int)StatType.Experience].Get(StatProperty.Max))
            {
                target.Stats[(int)StatType.Experience].AddToRoot(StatProperty.Value, amount);
                target.LevelUp();
            }
            else target.Stats[(int)StatType.Experience].AddToRoot(StatProperty.Value, amount);
        }
        /// <summary>
        /// Divide some value by a Stat. For example when applying damage, divide the incoming damage by the Endurance stat.
        /// </summary>
	    public static float ExampleValueDivByStat(float inValue, Stat stat, StatProperty property)
	    {
            return inValue / Mathf.Clamp(stat.Get(property) / 10, 1, 99999);
        }
	    /// <summary>
	    /// Multiply some value by a Stat. For example when applying damage, multiply the base damage by the caster's Strength stat.
	    /// </summary>
        public static float ExampleValueMultByStat(float inValue, Stat stat, StatProperty property)
	    {
	        return inValue * Mathf.Clamp(stat.Get(property) / 10, 1, 99999);
	    }
    }
}