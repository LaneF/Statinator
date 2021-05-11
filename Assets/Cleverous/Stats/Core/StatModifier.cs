// (c) Copyright Cleverous 2017. All rights reserved.

using System;
using UnityEngine;

namespace Cleverous.Stats
{
    [CreateAssetMenu(fileName = "New Stat Modifier", menuName = "Cleverous/Stat System/Stat Modifier", order = 700)]
    public class StatModifier : ScriptableObject
    {
        public StatModifier(string title, string description, StatType stat, StatModEffect effect, StatModType modType, StatProperty property, float value, float time, bool cumulative)
        {
            Title = title;
            Description = description;

            TargetStat = stat;
            TargetProperty = property;

            ModType = modType;
            ModEffect = effect;
            ModifierValue = value;
            ModifierTime = time;

            Cumulative = cumulative;
            _initialized = false;
        }

        public float GetEffectDelta(Stat target)
        {
            if (!_initialized)
            {
                _initialized = true;
                _sourceVal = Cumulative ? target.Get(TargetProperty) : target.GetRoot(TargetProperty);
            }
            switch (ModEffect)
            {
                case StatModEffect.Add:         return ModifierValue;
                case StatModEffect.Multiply:    return _sourceVal * ModifierValue - _sourceVal;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        public void Update()
        {
            if (ModType == StatModType.Active) ModifierTime -= Time.deltaTime;
        }

        public string Title;
        [TextArea]
        public string Description;
        public StatType TargetStat;
        public StatProperty TargetProperty;
        public StatModType ModType;
        public StatModEffect ModEffect;
        public float ModifierValue;
        public float ModifierTime;
        public bool Cumulative;

        private float _sourceVal;
        private bool _initialized;
    }
}