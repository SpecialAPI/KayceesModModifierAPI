using System;
using System.Collections.Generic;
using System.Text;
using DiskCardGame;
using HarmonyLib;

namespace KayceesModModifierAPI
{
    [HarmonyPatch(typeof(AscensionChallengesUtil), "GetInfo")]
    public class AscensionChallengesUtil_GetInfo
    {
        [HarmonyPostfix]
        public static void Postfix(ref AscensionChallengeInfo __result, AscensionChallenge challenge)
        {
            if (__result == null && NewChallenge.allChallenges.Exists((x) => x != null && x.challenge != null && x.challenge.challengeType == challenge))
            {
                __result = NewChallenge.allChallenges.Find((x) => x != null && x.challenge != null && x.challenge.challengeType == challenge).challenge;
            }
        }
    }
}
