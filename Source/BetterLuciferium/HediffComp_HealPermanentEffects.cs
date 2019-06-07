using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace BetterLuciferium
{
    class HediffComp_HealPermanentEffects : HediffComp
    {

        private int ticksToHeal;

        public HediffCompProperties_HealPermanentEffects Props
        {
            get
            {
                return (HediffCompProperties_HealPermanentEffects)this.props;
            }
        }

        public override void CompPostMake()
        {
            base.CompPostMake();
            this.ResetTicksToHeal();
        }

        private void ResetTicksToHeal()
        {
            //Vanilla heal-scars effect uses Rand.Range(15, 30) * 60000;
            //60000 ticks = 1 in-game day
            this.ticksToHeal = (int) Math.Round(Rand.Range(Props.cureMinDays, Props.cureMaxDays) * 60000);
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (ticksToHeal == 0)
            {
                //Mod was loaded on an existing save, so CompPostMake() was not called as expected. Reset ticks to fix state.
                this.ResetTicksToHeal();
            }

            this.ticksToHeal--;
            if (this.ticksToHeal <= 1)  //using 1 instead of 0 to accomodate existing-save check above.
            {
                this.TryHealRandomPermanentEffect();
                this.ResetTicksToHeal();
            }
        }

        private void TryHealRandomPermanentEffect()
        {
            IEnumerable <Hediff> hediffs = base.Pawn.health.hediffSet.hediffs;
            List<Hediff> curableHediffs = new List<Hediff>();
            foreach (Hediff hediff in hediffs)
            {
                if (IsCurableHealthEffect(hediff, base.Pawn))
                {
                    curableHediffs.Add(hediff);
                }
            }

            if (curableHediffs.Count > 0)
            {
                Random rnd = new Random();
                int selectedHediffIndex = rnd.Next(0, curableHediffs.Count - 1);
                Hediff targetHediff = curableHediffs.ElementAt(selectedHediffIndex);

                base.Pawn.health.RemoveHediff(targetHediff);

                if (PawnUtility.ShouldSendNotificationAbout(base.Pawn))
                {
                    String notificationMessage = "BetterLuciferium_curedHediff_notification".Translate(base.Pawn.LabelShort, targetHediff.Label, this.parent.LabelCap);
                    Messages.Message(notificationMessage, base.Pawn, MessageTypeDefOf.PositiveEvent, true);
                }
            }
        }

        private Boolean IsCurableHealthEffect(Hediff hediff, Pawn pawn)
        {
            foreach (HediffDef curableHediffDef in Props.curableHediffs)
            {
                if (curableHediffDef.Equals(hediff.def)) {
                    return true;
                }
            }
            return false;
        }

        public override void CompExposeData()
        {
            Scribe_Values.Look<int>(ref this.ticksToHeal, "BetterLuciferium_ticksToHealEffect", 0, false);
        }

        public override string CompDebugString()
        {
            return "BetterLuciferium_ticksToHealEffect: " + this.ticksToHeal;
        }

	}
}
