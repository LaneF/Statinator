// (c) Copyright Cleverous 2017. All rights reserved.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cleverous.Stats
{
    // TODO SetDirty? Improve loop performance.

    /// <summary>
    /// <para>Stats are good for any scalable or adjustable values attached to an entity. </para>
    /// <para>STR/END/DEX are examples of 'Regulated' stats. (whos value is defined by affinity and current level)</para>
    /// <para>HP/LVL/XP are examples of 'Unregulated' stats. (whos value is not constantly calculated with formulas)</para>
    /// </summary>
    [Serializable]
    public class Stat
    {
        // Modified and Root values are separate!
        // Get() will return the Root + Modified values.
        // GetRootValue() will return [only] the Root value, without considering modifiers.
        // GetModifiedValue() will return the value of [only] the modifiers on the given property.

        public bool UnregulatedValue;
        public IUseStats Owner;
        public List<StatModifier> Modifiers;
        public bool IsReady;

        // original/root/base
        [SerializeField] private float _rBase;
        [SerializeField] private float _rActual;
        [SerializeField] private float _rMin;
        [SerializeField] private float _rMax;
        [SerializeField] private float _rAffinity;
        [SerializeField] private float _rMaxAffinity;

        // modifiers
        private float _mBase;
        private float _mActual;
        private float _mMin;
        private float _mMax;
        private float _mAffinity;
        private float _mMaxAffinity;

        // totals
        private float _tBase;
        private float _tActual;
        private float _tMin;
        private float _tMax;
        private float _tAffinity;
        private float _tMaxAffinity;
        
        // It seems to me that all three layers of stats are needed. The Root(r), Modifiers(m), and True(t) layers.
        // T = R + M
        // M = total of modifier values (often zero)
        // R = root base stat values

        /// <summary>
        /// <para>Create a new stat!</para>
        /// </summary>
        /// <param name="unregulated">If true, the <see cref="StatType"/>.Value will be calculated with the Stat Formula every Update.</param>
        /// <param name="sBase">The Base value of the <see cref="Stat"/></param>
        /// <param name="min">The minimum value. It cannot be set below this value.</param>
        /// <param name="max">The maximum value. It cannot be set above this value.</param>
        /// <param name="sAffinity">How much the <see cref="StatType"/>.Value property gains per level.</param>
        /// <param name="sAffinityMax">How much the <see cref="StatType"/>.Max property gains per level.</param>
        /// <param name="actual">The starting value. Regulated stats are automatically calculated and Updated with new 'actual' values.</param>
        public Stat(bool unregulated, float sBase = 3, float min = 0, float max = 1000, float sAffinity = 1, float sAffinityMax = 1, float actual = 10)
        {
            UnregulatedValue    = unregulated;
            _rBase               = sBase;
            _rMin                = min;
            _rMax                = max;
            _rActual             = actual;
            _rAffinity           = sAffinity;
            _rMaxAffinity        = sAffinityMax;
        }

        public virtual void Initialize(IUseStats owner)
        {
            Owner = owner;
            IsReady = true;
            Modifiers = new List<StatModifier>();
            DoUpdate();
        }
        public virtual void DoUpdate()
        {
            #region Modifiers
            _mBase = 0;
            _mActual = 0;
            _mMin = 0;
            _mMax = 0;
            _mAffinity = 0;
            _mMaxAffinity = 0;

            if (Modifiers.Count > 0)
            {
                for (int i = 0; i < Modifiers.Count; i++)
                {
                    StatModifier m = Modifiers[i];
                    if (m.ModType == StatModType.Active && m.ModifierTime > 0)
                    {
                        m.Update();
                        AddToMod(m.TargetProperty, m.GetEffectDelta(this));
                    }
                    else
                    {
                        AddToMod(m.TargetProperty, m.GetEffectDelta(this));
                        Modifiers.Remove(m);
                    }
                }
            }
            //Debug.Log(string.Format("Base: {0}, Value: {1}, Min: {2}, Max: {3}, Aff: {4}, mAf: {5}", _mBase, _mActual, _mMin, _mMax, _mAffinity, _mMaxAffinity));
            #endregion

            #region True Values

            _tMin           = _rMin + _mMin;
            _tMax           = _rMax + Owner.Stats[(int)StatType.Level].Get(StatProperty.Value) * Get(StatProperty.MaxAffinity);
            _tBase          = _rBase + _mBase; 
            _tAffinity      = _rAffinity + _mAffinity;
            _tMaxAffinity   = _rMaxAffinity + _mMaxAffinity;
            _tActual = UnregulatedValue
                ? Mathf.Clamp(_rActual + _mActual, _tMin, _tMax)
                : StatUtility.GetRegulatedStatValue(this) + _mActual;
            if (!UnregulatedValue) _rActual = StatUtility.GetRegulatedStatValue(this);
            #endregion
        }

        /// <summary>
        /// Get a property of this stat.
        /// </summary>
        /// <param name="property">Which property of this stat do you want?</param>
        /// <returns>The true value of the target stat and property, including any modifiers affecting it.</returns>
        public virtual float Get(StatProperty property)
        {
            switch (property)
            {
                case StatProperty.Base:         return _tBase;
                case StatProperty.Value:        return _tActual;
                case StatProperty.Min:          return _tMin;
                case StatProperty.Max:          return _tMax;
                case StatProperty.Affinity:     return _tAffinity;
                case StatProperty.MaxAffinity:  return _tMaxAffinity;
                default: throw new ArgumentOutOfRangeException("property", property, null);
            }
        }

        /// <summary>
        /// Get a property of this stat.
        /// </summary>
        /// <param name="property">Which property of this stat do you want?</param>
        /// <returns>The root-only value of the target stat and property, excluding modifiers.</returns>
        public virtual float GetRoot(StatProperty property)
        {
            switch (property)
            {
                case StatProperty.Base:         return _rBase;
                case StatProperty.Value:        return _rActual;
                case StatProperty.Min:          return _rMin;
                case StatProperty.Max:          return _rMax;
                case StatProperty.Affinity:     return _rAffinity;
                case StatProperty.MaxAffinity:  return _rMaxAffinity;
                default: throw new ArgumentOutOfRangeException("property", property, null);
            }
        }
        public virtual void SetRoot(StatProperty property, float value)
        {
            switch (property)
            {
                case StatProperty.Value:
                    _rActual = Mathf.Clamp(value, _tMin, _tMax);
                    break;
                case StatProperty.Base:
                    _rBase = Mathf.Clamp(value, _tMin, _tMax);
                    break;
                case StatProperty.Min:
                    _rMin = value;
                    break;
                case StatProperty.Max:
                    _rMax = value;
                    break;
                case StatProperty.Affinity:
                    _rAffinity = value;
                    break;
                case StatProperty.MaxAffinity:
                    _rMaxAffinity = value;
                    break;
                default: throw new ArgumentOutOfRangeException("property", property, null);
            }
        }
        public virtual bool AddToRoot(StatProperty property, float value)
        {
            SetRoot(property, GetRoot(property) + value);
            return true;
        }

        /// <summary>
        /// Get the value of modifiers on a property of this stat.
        /// </summary>
        /// <param name="property">Which property on this stat?</param>
        /// <returns>The total value of modifiers being applied to this stat and property.</returns>
        public virtual float GetMod(StatProperty property)
        {
            switch (property)
            {
                case StatProperty.Value:        return _mActual;
                case StatProperty.Base:         return _mBase;
                case StatProperty.Min:          return _mMin;
                case StatProperty.Max:          return _mMax;
                case StatProperty.Affinity:     return _mAffinity;
                case StatProperty.MaxAffinity:  return _mMaxAffinity;
                default: throw new ArgumentOutOfRangeException("property", property, null);
            }
        }
        /// <summary>
        /// Add to the modifier value for this frame only.
        /// </summary>
        /// <param name="property">Which property of the stat?</param>
        /// <param name="value">How much to add. Can be negative.</param>
        public virtual void AddToMod(StatProperty property, float value)
        {
            switch (property)
            {
                case StatProperty.Value:
                    _mActual += value;
                    break;
                case StatProperty.Base:
                    _mBase += value;
                    break;
                case StatProperty.Min:
                    _mMin += value;
                    break;
                case StatProperty.Max:
                    _mMax += value;
                    break;
                case StatProperty.Affinity:
                    _mAffinity += value;
                    break;
                case StatProperty.MaxAffinity:
                    _mMaxAffinity += value;
                    break;
                default: throw new ArgumentOutOfRangeException("property", property, null);
            }
        }
    }
}