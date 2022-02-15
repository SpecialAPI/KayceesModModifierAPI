using DiskCardGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KayceesModModifierAPI
{
    public static class KayceeModModifier
    {
        public static void AddStarterDeckOption(string name, Texture2D icon, List<string> cards, int requiredLevelForUnlock)
        {
            AddStarterDeckOption(name, Sprite.Create(icon, new Rect(0f, 0f, icon.width, icon.height), new Vector2(0.5f, 0.5f)), cards, requiredLevelForUnlock);
        }

        public static void AddStarterDeckOption(string name, Sprite icon, List<string> cards, int requiredLevelForUnlock, List<KayceeModUnlockCondition> extraUnlockConditions = null)
        {
            if(cards.Count > 0)
            {
                CustomStarterDeckInfo info = ScriptableObject.CreateInstance<CustomStarterDeckInfo>();
                info.cards = cards;
                info.iconSprite = icon;
                info.title = name;
                info.name = name;
                info.unlockLevel = requiredLevelForUnlock;
                if(extraUnlockConditions != null)
                {
                    info.extraUnlockConditions = new List<KayceeModUnlockCondition>(extraUnlockConditions);
                }
                CustomAddedDecks.Add(info);
            }
        }

        public static void AddStarterDeckOption(string name, Texture2D icon, List<CardInfo> cards, int requiredLevelForUnlock)
        {
            AddStarterDeckOption(name, Sprite.Create(icon, new Rect(0f, 0f, icon.width, icon.height), new Vector2(0.5f, 0.5f)), cards.ConvertAll((x) => x.name), requiredLevelForUnlock);
        }

        public static void AddStarterDeckOption(string name, Sprite icon, List<CardInfo> cards, int requiredLevelForUnlock)
        {
            AddStarterDeckOption(name, icon, cards.ConvertAll((x) => x.name), requiredLevelForUnlock);
        }

        public static List<CustomStarterDeckInfo> CustomAddedDecks
        {
            get
            {
                if (addedCustomDecks == null)
                {
                    addedCustomDecks = new List<CustomStarterDeckInfo>();
                }
                return addedCustomDecks;
            }
        }

        private static List<CustomStarterDeckInfo> addedCustomDecks;
    }
}
