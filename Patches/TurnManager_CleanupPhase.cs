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
			foreach(ChallengeBehaviour behav in ChallengeBehaviour.Instances)
            {
				if(behav != null && behav.RespondToPreCleanup())
                {
					yield return behav.OnPreCleanup();
                }
            }
			yield return result;
			foreach (ChallengeBehaviour behav in ChallengeBehaviour.Instances)
			{
				if (behav != null && behav.RespondToPostCleanup())
				{
					yield return behav.OnPostCleanup();
				}
			}
			ChallengeBehaviour.DestroyAllInstances();
			yield break;
		}
	}
}
