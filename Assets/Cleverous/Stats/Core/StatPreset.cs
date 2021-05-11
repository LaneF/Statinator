// (c) Copyright Cleverous 2017. All rights reserved.

using UnityEngine;

namespace Cleverous.Stats
{
    [CreateAssetMenu(fileName = "New Stat Preset", menuName = "Cleverous/Stat System/Stat Preset", order = 700)]
    public class StatPreset : ScriptableObject
    {
        public Stat[] Stats; 

        public void Reset()
        {
            Stats = StatUtility.BaseCharacterStats();
        }
    }
}