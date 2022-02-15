using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using System.Text;
using DiskCardGame;

namespace KayceesModModifierAPI
{
    [HarmonyPatch(typeof(AscensionUnlockSchedule), "ChallengeIsUnlockedForLevel")]
    public class AscensionUnlockSchedule_ChallengeIsUnlockedForLevel
    {
        [HarmonyPostfix]
        public static void Postfix(ref bool __result, AscensionChallenge challenge, int level)
        {
            if (NewChallenge.allChallenges.Exists((x) => x != null && x.challenge != null && x.challenge.challengeType == challenge && x.levelRequiredForUnlock <= level && x.ExtraUnlocksSatisfied(level)))
            {
                __result = true;
            }
        }
    }
}
