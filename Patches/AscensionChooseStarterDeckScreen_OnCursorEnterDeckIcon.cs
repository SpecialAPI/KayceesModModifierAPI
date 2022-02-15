using System;
using System.Collections.Generic;
using System.Text;
using DiskCardGame;
using GBC;
using HarmonyLib;
using UnityEngine;

namespace KayceesModModifierAPI
{
    [HarmonyPatch(typeof(AscensionChooseStarterDeckScreen), "OnCursorEnterDeckIcon")]
    public class AscensionChooseStarterDeckScreen_OnCursorEnterDeckIcon
    {
        [HarmonyPrefix]
        public static bool Prefix(AscensionChooseStarterDeckScreen __instance, AscensionStarterDeckIcon icon)
        {
            if(__instance.GetComponent<AscensionDeckSelectTempCardManager>() != null)
            {
                __instance.GetComponent<AscensionDeckSelectTempCardManager>().tempCards = __instance.GetComponent<AscensionDeckSelectTempCardManager>().tempCards ?? new List<GameObject>();
                __instance.GetComponent<AscensionDeckSelectTempCardManager>().tempCards?.ForEach(delegate(GameObject x) { if (x != null) { UnityEngine.Object.Destroy(x); } });
                __instance.GetComponent<AscensionDeckSelectTempCardManager>().tempCards?.Clear();
            }
            if(__instance.cards != null && icon != null && icon.Info != null && icon.Info.cards != null && icon.Info.cards.Count != __instance.cards.Count && icon.Unlocked)
            {
                __instance.cards?.ForEach(delegate (PixelSelectableCard x)
                {
                    x?.gameObject?.SetActive(false);
                });
                List<GameObject> tempCards = new List<GameObject>();
                for(int i = 0; i < icon.Info.cards.Count; i++)
                {
                    CardInfo ci = icon.Info.cards[i];
                    if(ci != null)
                    {
                        float distance = __instance.cards[1].transform.position.x - __instance.cards[0].transform.position.x;
                        GameObject tempCard = UnityEngine.Object.Instantiate(__instance.cards[0].gameObject, __instance.cards[1].transform.position + (Vector3.right * ((icon.Info.cards.Count / 2 - 0.5f) * -distance + i * distance)), Quaternion.identity);
                        PixelSelectableCard sc = tempCard.GetComponent<PixelSelectableCard>();
                        sc.SetInfo(ci);
                        sc.SetEnabled(false);
                        tempCard.SetActive(true);
                        tempCard.transform.parent = __instance.cards[0].transform.parent;
                        tempCards.Add(tempCard);
                    }
                }
                __instance.StopAllCoroutines();
                __instance.StartCoroutine(__instance.ShowCardsSequence());
                AscensionDeckSelectTempCardManager manager = __instance.GetComponent<AscensionDeckSelectTempCardManager>() ?? __instance.gameObject.AddComponent<AscensionDeckSelectTempCardManager>();
                manager.tempCards = manager.tempCards ?? new List<GameObject>();
                manager.tempCards?.AddRange(tempCards);
                return false;
            }
            else
            {
                __instance.cards?.ForEach(delegate (PixelSelectableCard x)
                {
                    x?.gameObject?.SetActive(true);
                });
            }
            return true;
        }
    }
}
