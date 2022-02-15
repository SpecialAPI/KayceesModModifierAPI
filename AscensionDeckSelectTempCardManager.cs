using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using DiskCardGame;

namespace KayceesModModifierAPI
{
    public class AscensionDeckSelectTempCardManager : ManagedBehaviour
    {
        public IEnumerator SequentiallyShowTempCards(List<GameObject> cards)
        {
			cards.ForEach(delegate (GameObject x)
			{
				x?.SetActive(false);
			});
			foreach (GameObject obj in cards)
			{
				yield return new WaitForSeconds(0.1f);
				obj?.SetActive(true);
				CommandLineTextDisplayer.PlayCommandLineClickSound();
			}
			yield break;
        }

        public List<GameObject> tempCards = new List<GameObject>();
    }
}
