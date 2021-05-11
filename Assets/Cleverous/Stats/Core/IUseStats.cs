// (c) Copyright Cleverous 2017. All rights reserved.

using UnityEngine;

namespace Cleverous.Stats
{
    public enum StatProperty { Value, Base, Min, Max, Affinity, MaxAffinity }
    public enum StatModType { Active, Direct }
    public enum StatModEffect { Add, Multiply }

    public interface IUseStats
    {
        Transform MyTransform { get; }

        StatPreset StatPreset { get; set; }
        Stat[] Stats { get; set; }
        bool IsDead { get; set; }

        void Damage(float amount, IUseStats source);
        void AddStatModifier(StatModifier mod);
        void RemoveStatModifier(StatModifier mod);

        float GetStatValue(StatType stat);
        float GetStatMax(StatType stat);

        void UpdateStats();
        void AddExp(float amount);
        void LevelUp();
    }
}