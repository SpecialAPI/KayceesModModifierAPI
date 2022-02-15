using System;
using System.Collections.Generic;
using System.Text;
using DiskCardGame;
using HarmonyLib;
using System.Linq;
using UnityEngine;

namespace KayceesModModifierAPI
{
    [HarmonyPatch(typeof(AscensionMenuScreens), "Start")]
    public class AscensionMenuScreens_Start
    {
        [HarmonyPostfix]
        public static void Postfix(AscensionMenuScreens __instance)
        {
            if(__instance.challengeUnlockSummaryScreen != null && __instance.challengeUnlockSummaryScreen.GetComponent<AscensionMenuScreenTransition>() != null)
            {
                GameObject challengeScreen = __instance.challengeUnlockSummaryScreen;
                AscensionMenuScreenTransition screen = challengeScreen.GetComponent<AscensionMenuScreenTransition>();
                List<AscensionIconInteractable> icons = new List<AscensionIconInteractable>(screen.screenInteractables.FindAll((x) => x.GetComponent<AscensionIconInteractable>() != null).ConvertAll((x) => 
                    x.GetComponent<AscensionIconInteractable>()));
                if(icons.Count > 0 && challengeScreen.GetComponent<AscensionChallengeSelectPageManager>() == null)
                {
                    List<AscensionChallengeInfo> challengesToAdd = new List<AscensionChallengeInfo>(NewChallenge.allChallenges.ConvertAll((x) => x.challenge.Repeat(x.appearancesInChallangeScreen)).SelectMany((x) => x));
                    icons.ForEach(delegate (AscensionIconInteractable ic)
                    {
                        if (ic != null && ic.Info == null && challengesToAdd.Count > 0)
                        {
                            ic.challengeInfo = challengesToAdd[0];
                            ic.AssignInfo(challengesToAdd[0]);
                            challengesToAdd.RemoveAt(0);
                        }
                    });
                    List<List<AscensionChallengeInfo>> pagesToAdd = new List<List<AscensionChallengeInfo>>();
                    while (challengesToAdd.Count > 0)
                    {
                        List<AscensionChallengeInfo> page = new List<AscensionChallengeInfo>();
                        for (int i = 0; i < icons.Count; i++)
                        {
                            if (challengesToAdd.Count > 0)
                            {
                                page.Add(challengesToAdd[0]);
                                challengesToAdd.RemoveAt(0);
                            }
                        }
                        pagesToAdd.Add(page);
                    }
                    if (pagesToAdd.Count > 0)
                    {
                        AscensionChallengeSelectPageManager manager = challengeScreen.gameObject.AddComponent<AscensionChallengeSelectPageManager>();
                        manager.Initialize(null, screen);
                        foreach (List<AscensionChallengeInfo> page in pagesToAdd)
                        {
                            manager.AddPage(page);
                        }
                        Vector3 topRight = new Vector3(float.MinValue, float.MinValue);
                        Vector3 bottomLeft = new Vector3(float.MaxValue, float.MaxValue);
                        foreach (AscensionIconInteractable icon in icons)
                        {
                            if (icon != null && icon.iconRenderer != null)
                            {
                                if (icon.iconRenderer.transform.position.x < bottomLeft.x)
                                {
                                    bottomLeft.x = icon.iconRenderer.transform.position.x;
                                }
                                if (icon.iconRenderer.transform.position.x > topRight.x)
                                {
                                    topRight.x = icon.iconRenderer.transform.position.x;
                                }
                                if (icon.iconRenderer.transform.position.y < bottomLeft.y)
                                {
                                    bottomLeft.y = icon.iconRenderer.transform.position.y;
                                }
                                if (icon.iconRenderer.transform.position.y > topRight.y)
                                {
                                    topRight.y = icon.iconRenderer.transform.position.y;
                                }
                            }
                        }
                        GameObject leftArrow = UnityEngine.Object.Instantiate(__instance.cardUnlockSummaryScreen.GetComponent<AscensionCardsSummaryScreen>().pageLeftButton.gameObject);
                        leftArrow.transform.parent = challengeScreen.transform;
                        leftArrow.transform.position = Vector3.Lerp(new Vector3(bottomLeft.x, topRight.y, topRight.z), new Vector3(bottomLeft.x, bottomLeft.y, topRight.z), 0.5f) + Vector3.left / 2f;
                        leftArrow.GetComponent<AscensionMenuInteractable>().ClearDelegates();
                        leftArrow.GetComponent<AscensionMenuInteractable>().CursorSelectStarted += (x) => manager.PreviousPage();
                        GameObject rightArrow = UnityEngine.Object.Instantiate(__instance.cardUnlockSummaryScreen.GetComponent<AscensionCardsSummaryScreen>().pageRightButton.gameObject);
                        rightArrow.transform.parent = challengeScreen.transform;
                        rightArrow.transform.position = Vector3.Lerp(new Vector3(topRight.x, topRight.y, topRight.z), new Vector3(topRight.x, bottomLeft.y, topRight.z), 0.5f) + Vector3.right / 2f;
                        rightArrow.GetComponent<AscensionMenuInteractable>().ClearDelegates();
                        rightArrow.GetComponent<AscensionMenuInteractable>().CursorSelectStarted += (x) => manager.NextPage();
                    }
                }
            }
        }
    }
}
