using System.Text;
using HarmonyLib;
using DiskCardGame;
using UnityEngine;
using System.Collections;

namespace KayceesModModifierAPI
{
    [HarmonyPatch(typeof(TurnManager), "CleanupPhase")]
    public class TurnManager_CleanupPhase
    {
		public static IEnumerator Postfix(IEnumerator result)
		{
			foreach(ChallengeBehavior behav in ChallengeBehavior.Instances)
            {
				if(behav != null && behav.RespondToPreCleanup())
                {
					yield return behav.OnPreCleanup();
                }
            }
			yield return result;
			foreach (ChallengeBehavior behav in ChallengeBehavior.Instances)
			{
				if (behav != null && behav.RespondToPostCleanup())
				{
					yield return behav.OnPostCleanup();
				}
			}
			ChallengeBehavior.DestroyAllInstances();
			yield break;
		}
	}
}
