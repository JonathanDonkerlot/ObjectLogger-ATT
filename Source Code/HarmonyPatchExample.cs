using HarmonyLib;
using MelonLoader;

namespace ObjectLogger
{
    [HarmonyPatch(typeof(TargetClass), "TargetMethod")]
    public static class HarmonyPatchExample
    {
        static void Postfix(object __instance)
        {
            MelonLogger.Msg($"TargetMethod was called on {__instance}");
        }
    }
    internal class TargetClass
    {
        public void TargetMethod() { }
    }
}
