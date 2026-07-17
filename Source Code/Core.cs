using HarmonyLib;
using MelonLoader;
using UnityEngine.InputSystem;

[assembly: MelonInfo(typeof(ObjectLogger.Core), "ObjectLogger", "0.1.0", "TheThinker")]
[assembly: MelonGame("Alta", "A Township Tale")]

namespace ObjectLogger
{
    public class Core : MelonMod
    {
        

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("MyModName loaded successfully.");
            HarmonyInstance.PatchAll();
        }

        public override void OnUpdate()
        {
            if (InputHelper.GetKeyDown(Key.L))
            {
                GameObjectLogger.DumpScene();
            }
        }
    }
}
