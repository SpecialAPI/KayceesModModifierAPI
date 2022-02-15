using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using System.Text;
using DiskCardGame;

namespace KayceesModModifierAPI
{
    [HarmonyPatch(typeof(AscensionUnlockSchedule), "StarterDeckIsUnlockedForLevel")]
    public class AscensionUnlockSchedule_StarterDeckIsUnlockedForLevel
    {
        [HarmonyPostfix]
        public static void Postfix(ref bool __result, string id, int level)
        {
            if(KayceeModModifier.CustomAddedDecks.Exists((x) => x.name.ToLower() == id.ToLower() && x.unlockLevel <= level && x.ExtraUnlocksSatisfied(level)))
            {
                __result = true;
            }
        }
    }
}
