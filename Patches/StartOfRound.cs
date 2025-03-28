using HarmonyLib;

namespace FreeThinkers.Patches;

[HarmonyPatch(typeof(StartOfRound))]
internal class StartOfRoundPatch
{
    
    [HarmonyPatch("Start")]
    private static void Prefix() {

        AnimatedItem_Patches.Load();
        RadMechAI_Patches.Load();

    }

}