using System;
using System.Collections.Generic;
using System.Text;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using UnityEngine;

namespace KayceesModModifierAPI
{
    [HarmonyPatch(typeof(TurnManager), "SetupPhase")]
    public class TurnManager_SetupPhase
    {
        public static IEnumerator Postfix(IEnumerator result, TurnManager __instance)
        {
            ChallengeBehavior.DestroyAllInstances();
            if (SaveFile.IsAscension && AscensionSaveData.Data != null && AscensionSaveData.Data.activeChallenges != null)
            {
                foreach (AscensionChallenge challenge in AscensionSaveData.Data.activeChallenges)
                {
                    NewChallenge nc = NewChallenge.allChallenges.Find((x) => x != null && x.challenge != null && x.challenge.challengeType == challenge);
                    if (nc != null && nc.challengeHandlerType != null && nc.challengeHandlerType.IsSubclassOf(typeof(ChallengeBehavior)))
                    {
                        GameObject challengehandler = new GameObject(nc.challenge.name + " Challenge Handler");
                        ChallengeBehavior behav = challengehandler.AddComponent(nc.challengeHandlerType) as ChallengeBehavior;
                        if (behav != null)
                        {
                            GlobalTriggerHandler.Instance?.RegisterNonCardReceiver(behav);
                            behav.challenge = nc;
                            ChallengeBehavior.Instances.Add(behav);
                            if (behav.RespondToPreBattleStart())
                            {
                                yield return behav.OnPreBattleStart();
                            }
                        }
                    }
                }
            }
            yield return result;
            foreach (ChallengeBehavior behav in ChallengeBehavior.Instances)
            {
                if (behav != null && behav.RespondToBattleStart())
                {
                    yield return behav.OnBattleStart();
                }
            }
            yield break;
        }
    }
}
