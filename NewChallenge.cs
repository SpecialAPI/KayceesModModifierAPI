using DiskCardGame;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace KayceesModModifierAPI
{
    public class NewChallenge
    {
        public static List<NewChallenge> allChallenges = new List<NewChallenge>();
        public AscensionChallengeInfo challenge;
        public int levelRequiredForUnlock;
        public Type challengeHandlerType;
        public int appearancesInChallangeScreen;
        public List<KayceeModUnlockCondition> extraUnlockConditions = new List<KayceeModUnlockCondition>();

        public bool ExtraUnlocksSatisfied(int level)
        {
            return extraUnlockConditions == null || extraUnlockConditions.ConvertAll((x) => x == null || x(level)).AllEqual(true);
        }

        public NewChallenge(string name, int challengePoints, string description, Texture2D icon, Texture2D activatedEyes, Type challengeHandlerType, int levelRequiredForUnlock, int numAppearancesInChallangeScreen = 1, 
            List<KayceeModUnlockCondition> extraUnlockConditions = null)
        {
            AscensionChallengeInfo info = ScriptableObject.CreateInstance<AscensionChallengeInfo>();
            info.name = name;
            info.title = name;
            info.pointValue = challengePoints;
            info.description = description;
            info.iconSprite = Sprite.Create(icon, new Rect(0f, 0f, icon.width, icon.height), new Vector2(0.5f, 0.5f));
            info.activatedSprite = Sprite.Create(activatedEyes, new Rect(0f, 0f, activatedEyes.width, activatedEyes.height), new Vector2(0.5f, 0.5f));
            info.challengeType = AscensionChallenge.NUM_TYPES + allChallenges.Count + 1;
            challenge = info;
            this.levelRequiredForUnlock = levelRequiredForUnlock;
            if (extraUnlockConditions != null)
            {
                this.extraUnlockConditions = new List<KayceeModUnlockCondition>(extraUnlockConditions);
            }
            this.challengeHandlerType = challengeHandlerType;
            appearancesInChallangeScreen = numAppearancesInChallangeScreen;
            allChallenges.Add(this);
        }
    }
}
