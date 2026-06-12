using HarmonyLib;
using UnityEngine;

namespace AdofaiBugfixer.Patches
{
    public class VideoBackgroundEditorFix : IPatch
    {
        public void Initialize() { }
    }

    [HarmonyPatch(typeof(scnEditor), nameof(scnEditor.SwitchToEditMode))]
    public static class scnEditor_SwitchToEditMode_Patch
    {
        public static void Postfix(scnEditor __instance)
        {
            var vfx = scrVfxPlus.instance;

            if (vfx != null && vfx.videoBG != null)
            {
                vfx.videoBG.Pause();
                vfx.hasPlayed = false;

                vfx.videoBG.gameObject.SetActive(false);
                vfx.videoBG.gameObject.SetActive(true);

                if (__instance.selectedFloors != null && __instance.selectedFloors.Count > 0)
                {
                    var floor = __instance.selectedFloors[0];
                    var cond = scrConductor.instance;

                    double num4 = cond.separateCountdownTime ? (cond.crotchetAtStart * (double)cond.adjustedCountdownTicks) : 0.0;
                    double expectedTime = floor.entryTime - num4 + (double)vfx.vidOffset;

                    if (expectedTime >= 0)
                    {
                        vfx.videoBG.time = expectedTime;
                    }
                    else
                    {
                        vfx.videoBG.Stop();
                    }
                }
            }
        }
    }
}