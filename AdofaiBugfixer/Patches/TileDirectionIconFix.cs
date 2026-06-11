using HarmonyLib;

namespace AdofaiBugfixer.Patches
{
    public class TileRotationUIFix : IPatch
    {
        public void Initialize() { }
    }

    [HarmonyPatch(typeof(scnEditor), nameof(scnEditor.RotateSelection))]
    public static class scnEditor_RotateSelection_Patch
    {
        public static void Postfix()
        {
            scnEditor.instance?.OnSelectedFloorChange();
        }
    }

    [HarmonyPatch(typeof(scnEditor), nameof(scnEditor.RotateSelection180))]
    public static class scnEditor_RotateSelection180_Patch
    {
        public static void Postfix()
        {
            scnEditor.instance?.OnSelectedFloorChange();
        }
    }

    [HarmonyPatch(typeof(scnEditor), nameof(scnEditor.RotateFloor))]
    public static class scnEditor_RotateFloor_Patch
    {
        public static void Postfix()
        {
            scnEditor.instance?.OnSelectedFloorChange();
        }
    }

    [HarmonyPatch(typeof(scnEditor), nameof(scnEditor.RotateFloor180))]
    public static class scnEditor_RotateFloor180_Patch
    {
        public static void Postfix()
        {
            scnEditor.instance?.OnSelectedFloorChange();
        }
    }

    [HarmonyPatch(typeof(scnEditor), nameof(scnEditor.FlipSelection))]
    public static class scnEditor_FlipSelection_Patch
    {
        public static void Postfix()
        {
            scnEditor.instance?.OnSelectedFloorChange();
        }
    }

    [HarmonyPatch(typeof(scnEditor), nameof(scnEditor.FlipFloor))]
    public static class scnEditor_FlipFloor_Patch
    {
        public static void Postfix()
        {
            scnEditor.instance?.OnSelectedFloorChange();
        }
    }
}