using System.Collections;
using System.IO;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Networking;

using FreeThinkers.Utils;

namespace FreeThinkers.Patches;

[HarmonyPatch(typeof(RadMechAI))]
public class RadMechAI_Patches {

    public static AudioClip? RadMechShanty;

    public static void Load() {

        string filePath = Path.Combine(Paths.PluginPath, "FreeThinkers\\audio\\shanty3.mp3");
        SharedCoroutineStarter.StartCoroutine(LoadAudioClip(filePath));

    }

    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void Start_Postfix(RadMechAI __instance) {

        if (RadMechShanty is null) {
            FreeThinkers.Logger.LogError("Old Bird shanty clip is not initialized.");
            return;
        }
        __instance.LocalLRADAudio.clip = RadMechShanty;
        __instance.LocalLRADAudio.volume = __instance.LocalLRADAudio2.volume;
        __instance.LocalLRADAudio.maxDistance *= 1.5f;
        __instance.LocalLRADAudio2.volume = 0f;

    }

    private static IEnumerator LoadAudioClip(string filePath) {
        
        UnityWebRequest loader = UnityWebRequestMultimedia.GetAudioClip(filePath, AudioType.MPEG);
        loader.SendWebRequest();
        while (!loader.isDone) {
            yield return null;
        }
        AudioClip clip = DownloadHandlerAudioClip.GetContent(loader);
        clip.name = "RadMechShanty";
        RadMechShanty = clip;

    }

}