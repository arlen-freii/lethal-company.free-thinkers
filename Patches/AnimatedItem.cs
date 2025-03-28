using System.Collections;
using System.IO;
using BepInEx;
using FreeThinkers.Utils;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Networking;

namespace FreeThinkers.Patches;

[HarmonyPatch(typeof(AnimatedItem))]
public class AnimatedItem_Patches {

    public static AudioClip? RobotShantyClip;
    public static readonly string DefaultAudioName = "RobotToyCheer";

    [HarmonyPatch("EquipItem")]
    [HarmonyPrefix]
    public static void EquipItem_Prefix(AnimatedItem __instance) {
        
        if (RobotShantyClip is null) {
            FreeThinkers.Logger.LogError("Toy Robot shanty clip is not initialized.");
            return;
        }

        if (
            (__instance.grabAudio.name != RobotShantyClip.name || __instance.grabAudio != null) &&
             __instance.grabAudio.name == DefaultAudioName
        ) {
            __instance.grabAudio = RobotShantyClip;
        }

    } 

    public static void Load() {

        string filePath = Path.Combine(Paths.PluginPath, "FreeThinkers\\audio\\shanty2.mp3");
        SharedCoroutineStarter.StartCoroutine(LoadAudioClip(filePath));

    }

    private static IEnumerator LoadAudioClip(string filePath) {

        UnityWebRequest loader = UnityWebRequestMultimedia.GetAudioClip(filePath, AudioType.MPEG);
        loader.SendWebRequest();
        while (!loader.isDone) {
            yield return null;
        }
        AudioClip clip = DownloadHandlerAudioClip.GetContent(loader);
        clip.name = "ToyRobotShanty";
        RobotShantyClip = clip;

    }

}