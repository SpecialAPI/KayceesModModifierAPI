using BepInEx;
using HarmonyLib;
using System;

namespace KayceesModModifierAPI
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "spapi.inscryption.kayceemodmodifierapi";
        public const string NAME = "KayceesModModifierAPI";
        public const string VERSION = "1.0.0";
        
        public void Awake()
        {
            Harmony harm = new Harmony(GUID);
            harm.PatchAll();
        }
    }
}
