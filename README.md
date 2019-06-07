# Better Luciferium

Upgrades Luciferium to heal a number of permanent conditions in addition to scars.

## Description

The mod adds a new effect to luciferium that runs in parallel with the heal-scars effect. The new effect is half as fast as scar healing, taking between 30 to 60 days to fix one random health problem before moving on to the next.

By default, the following health conditions are curable by Better Luciferium:
- Frail
- Bad Back
- Cataracts
- Blindness
- Hearing Loss
- Dementia
- Alzheimers
- Asthma
- Artery Blockage
- Carcinoma
- Chemical Damage (Moderate)
- Chemical Damage (Severe)
- Cirrhosis

Note that luciferium treats the symptoms, not the cause. A colonist might develop bad back, get it fixed by luciferium, then possibly develop the condition again on one of their next birthdays, at which point the drug will work on fixing it again. For extremely old colonists with a large amount of health problems, it might be wise to fix some of the problems via bionics, so luciferium can focus on the remaining issues.

## Customizable

Want to change which health conditions luciferium can cure, or how long it takes between cures? It's a simple text edit in XML. In /Patches/Luciferium_Patch.xml, adjust <cureMinDays> and <cureMaxDays> to the desired values. You'll also find the list of hediff defNames that Better Luciferium targets in the <curableHediffs> element, add new ones or remove existing ones as you like.

If adding hediffs from a mod, you need to prefix the defName with the mod's namespace, like so: \<li\>ExampleMod.SomeCripplingHediff\</li\>

Changes will apply once the game client is restarted, and works for existing saves.

## Compatible

Nothing from the base game is overriden, so Better Luciferium should be compatible with the majority of mods that change luciferium mechanics.

If you're using a mod that adds new variants of luciferium, and want the new heal effect to apply to them, open /Patches/Luciferium_Patch.xml and duplicate the entire Operation element. Now, on the duplicated operation, replace defName="LuciferiumHigh" to target your mod's luciferium variant instead.

Like with adding hediffs from a mod, here you must also specify the mod's namespace, like so: defName="ExampleMod.CustomLuciferiumHigh"
