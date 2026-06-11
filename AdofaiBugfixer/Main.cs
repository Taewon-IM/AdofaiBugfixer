using System;
using System.Reflection;
using UnityModManagerNet;
using HarmonyLib;

namespace AdofaiBugfixer
{
    public static class Main
    {
        public static UnityModManager.ModEntry mod;
        public static Harmony harmony;
        public static bool isEnabled = false;

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            mod = modEntry;
            modEntry.OnToggle = OnToggle;

            return true;
        }

        public static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            isEnabled = value;

            if (isEnabled)
            {
                harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                mod.Logger.Log("ADOFAI Bugfixer Enabled");
            }
            else
            {
                harmony.UnpatchAll(modEntry.Info.Id);
                mod.Logger.Log("ADOFAI Bugfixer Disabled");
            }

            return true;
        }
    }
}