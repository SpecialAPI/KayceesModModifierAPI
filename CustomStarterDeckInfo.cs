using System;
using System.Collections.Generic;
using System.Text;
using DiskCardGame;
using UnityEngine;

namespace KayceesModModifierAPI
{
    public class CustomStarterDeckInfo : ScriptableObject
    {
		public string title = null;
		public Sprite iconSprite = null;
		public List<string> cards = null;
		public int unlockLevel;
		public List<KayceeModUnlockCondition> extraUnlockConditions = new List<KayceeModUnlockCondition>();

		public bool ExtraUnlocksSatisfied(int level)
        {
			return extraUnlockConditions == null || extraUnlockConditions.ConvertAll((x) => x == null || x(level)).AllEqual(true);
        }

		public StarterDeckInfo ToStarterDeck()
        {
			StarterDeckInfo deck = CreateInstance<StarterDeckInfo>();
			deck.cards = cards.ConvertAll((string s) => CardLoader.GetCardByName(s));
			deck.iconSprite = iconSprite;
			deck.title = title;
			deck.name = name;
			return deck;
        }
	}
}
