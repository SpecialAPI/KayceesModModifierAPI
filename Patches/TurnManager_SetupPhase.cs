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
            ChallengeBehaviour.DestroyAllInstances();
            if (SaveFile.IsAscension && AscensionSaveData.Data != null && AscensionSaveData.Data.activeChallenges != null)
            {
                foreach (AscensionChallenge challenge in AscensionSaveData.Data.activeChallenges)
                {
                    NewChallenge nc = NewChallenge.allChallenges.Find((x) => x != null && x.challenge != null && x.challenge.challengeType == challenge);
                    if (nc != null && nc.challengeHandlerType != null && nc.challengeHandlerType.IsSubclassOf(typeof(ChallengeBehaviour)))
                    {
                        GameObject challengehandler = new GameObject(nc.challenge.name + " Challenge Handler");
                        ChallengeBehaviour behav = challengehandler.AddComponent(nc.challengeHandlerType) as ChallengeBehaviour;
                        if (behav != null)
                        {
                            GlobalTriggerHandler.Instance?.RegisterNonCardReceiver(behav);
                            behav.challenge = nc;
                            ChallengeBehaviour.Instances.Add(behav);
                            if (behav.RespondToPreBattleStart())
                            {
                                yield return behav.OnPreBattleStart();
                            }
                        }
                    }
                }
            }
            yield return result;
            foreach (ChallengeBehaviour behav in ChallengeBehaviour.Instances)
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
