using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using DiskCardGame;
using System.Text;

namespace KayceesModModifierAPI
{
    [HarmonyPatch(typeof(StarterDecksUtil), "GetInfo")]
    public class StarterDecksUtil_GetInfo
    {
        [HarmonyPostfix]
        public static void Postfix(ref StarterDeckInfo __result, string deckId)
        {
            if (__result == null && KayceeModModifier.CustomAddedDecks.Exists((x) => x.name.ToLower() == deckId.ToLower()))
            {
                __result = KayceeModModifier.CustomAddedDecks.Find((x) => x.name.ToLower() == deckId.ToLower()).ToStarterDeck();
            }
        }
    }
}
