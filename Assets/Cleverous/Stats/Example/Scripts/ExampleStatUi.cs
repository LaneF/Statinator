// (c) Copyright Cleverous 2017. All rights reserved.

using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Cleverous.Stats
{
    public class ExampleStatUi : MonoBehaviour
    {
        public GameObject TargetGo;
        public IUseStats Target;
        public Text Name, Level, Experience, Health, Mana, Agility, Dexterity, Endurance, Strength, RegenHp, RegenMp;

        public void Awake()
        {
            if (TargetGo) Target = TargetGo.GetComponent<IUseStats>();
            if (Name) Name.text = TargetGo.GetComponent<StatsCharacter>().MyTitle;
        }
        protected void Update()
        {
            if (Target == null) return;

            if (Level)      Level.text          = Target.GetStatValue(StatType.Level)       .ToString(CultureInfo.InvariantCulture);
            if (Experience) Experience.text     = Target.GetStatValue(StatType.Experience)  .ToString(CultureInfo.InvariantCulture);
            if (Health)     Health.text         = Target.GetStatValue(StatType.Health)      .ToString(CultureInfo.InvariantCulture);
            if (Mana)       Mana.text           = Target.GetStatValue(StatType.Mana)        .ToString(CultureInfo.InvariantCulture);
            if (Agility)    Agility.text        = Target.GetStatValue(StatType.Agility)     .ToString(CultureInfo.InvariantCulture);
            if (Dexterity)  Dexterity.text      = Target.GetStatValue(StatType.Dexterity)   .ToString(CultureInfo.InvariantCulture);
            if (Endurance)  Endurance.text      = Target.GetStatValue(StatType.Endurance)   .ToString(CultureInfo.InvariantCulture);
            if (Strength)   Strength.text       = Target.GetStatValue(StatType.Strength)    .ToString(CultureInfo.InvariantCulture);
            if (RegenHp)    RegenHp.text        = Target.GetStatValue(StatType.RegenHp)     .ToString(CultureInfo.InvariantCulture);
            if (RegenMp)    RegenMp.text        = Target.GetStatValue(StatType.RegenMp)     .ToString(CultureInfo.InvariantCulture);
        }
    }
}