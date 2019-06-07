using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace BetterLuciferium
{
    public class HediffCompProperties_HealPermanentEffects : HediffCompProperties
    {
        public HediffCompProperties_HealPermanentEffects()
        {
            this.compClass = typeof(HediffComp_HealPermanentEffects);
        }

        public float cureMinDays;

        public float cureMaxDays;

        public List<HediffDef> curableHediffs = new List<HediffDef>();
    }
}
