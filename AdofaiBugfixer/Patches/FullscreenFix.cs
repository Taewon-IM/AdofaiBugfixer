using HarmonyLib;
using UnityEngine;

namespace AdofaiBugfixer.Patches
{
    public class FullscreenFix : IPatch
    {
        public void Initialize()
        {
            if (PlayerPrefs.HasKey("Bugfixer_IsFullscreen"))
            {
                bool isFullscreen = PlayerPrefs.GetInt("Bugfixer_IsFullscreen") == 1;
                Screen.fullScreenMode = isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            }
        }
    }
    [HarmonyPatch(typeof(SettingsMenu), "UpdateSetting")]
    public static class SettingsMenu_UpdateSetting_Patch
    {
        private static bool? tempFullscreen = null;

        public static bool Prefix(SettingsMenu __instance, PauseSettingButton setting, SettingsMenu.Interaction action)
        {
            if (setting.name != "fullscreen") return true;

            bool actualState = PlayerPrefs.GetInt("Bugfixer_IsFullscreen", Screen.fullScreen ? 1 : 0) == 1;

            if (action == SettingsMenu.Interaction.Refresh)
            {
                tempFullscreen = null;
                setting.initialValue = actualState;
                setting.CachedValue = actualState;
                setting.valueLabel.text = RDString.Get(actualState ? "pauseMenu.settings.on" : "pauseMenu.settings.off");
                setting.confirmText.gameObject.SetActive(false);
                return false;
            }
            bool currentState = tempFullscreen ?? actualState;

            if (action == SettingsMenu.Interaction.Increment || action == SettingsMenu.Interaction.Decrement)
            {
                currentState = !currentState;
                tempFullscreen = currentState;

                setting.valueLabel.text = RDString.Get(currentState ? "pauseMenu.settings.on" : "pauseMenu.settings.off");

                bool isChanged = (currentState != actualState);
                setting.confirmText.gameObject.SetActive(isChanged);

                var pauseMenu = scrController.instance.pauseMenu;
                setting.PlayArrowAnimation(action == SettingsMenu.Interaction.Increment);
                pauseMenu.PlayMenuSfx(action == SettingsMenu.Interaction.Increment ? SfxSound.MenuIncrement : SfxSound.MenuDecrement);

                return false;
            }

            if (action == SettingsMenu.Interaction.Activate)
            {
                var pauseMenu = scrController.instance.pauseMenu;
                pauseMenu.PlayMenuSfx(SfxSound.MobileButtonEnter);

                if (tempFullscreen.HasValue && tempFullscreen.Value != actualState)
                {
                    actualState = tempFullscreen.Value;

                    if (actualState)
                    {
                        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    }
                    else
                    {
                        Screen.fullScreenMode = FullScreenMode.Windowed;
                    }

                    PlayerPrefs.SetInt("Bugfixer_IsFullscreen", actualState ? 1 : 0);
                    PlayerPrefs.Save();
                }
                __instance.GetType().GetField("_lastModificationFrame", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.SetValue(__instance, Time.frameCount);

                tempFullscreen = null;
                setting.initialValue = actualState;
                setting.CachedValue = actualState;
                setting.valueLabel.text = RDString.Get(actualState ? "pauseMenu.settings.on" : "pauseMenu.settings.off");
                setting.confirmText.gameObject.SetActive(false);

                return false;
            }
            return false;
        }
    }
}