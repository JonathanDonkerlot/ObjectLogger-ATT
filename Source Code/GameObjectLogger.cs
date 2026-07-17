using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using MelonLoader;

public static class GameObjectLogger
{
    private static readonly string DumpPath =
        Path.Combine(
            Directory.GetCurrentDirectory(),
            "MelonLoader",
            "GameObjectDump.txt");

    public static void DumpScene()
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            Scene scene = SceneManager.GetActiveScene();

            sb.AppendLine("====================================================");
            sb.AppendLine("ATT GameObject Dump");
            sb.AppendLine("====================================================");
            sb.AppendLine($"Scene: {scene.name}");
            sb.AppendLine($"Time: {DateTime.Now}");
            sb.AppendLine();

            GameObject[] roots = scene.GetRootGameObjects();

            sb.AppendLine($"Found {roots.Length} GameObjects");
            sb.AppendLine();

            foreach (GameObject root in roots)
            {
                DumpTransform(root.transform, sb, 0);
            }

            string folder = Path.GetDirectoryName(DumpPath);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            File.WriteAllText(DumpPath, sb.ToString());

            MelonLogger.Msg($"Dump complete. Root objects: {roots.Length}");
            MelonLogger.Msg($"[GameObjectLogger] Saved to: {DumpPath}");
        }
        catch (Exception ex)
        {
            MelonLogger.Error("[GameObjectLogger] Failed to dump GameObjects!");
            MelonLogger.Error(ex.ToString());
        }
    }
    private static void DumpTransform(Transform t, StringBuilder sb, int depth)
    {
        string indent = new string(' ', depth * 2);

        sb.AppendLine($"{indent}{t.name}");

        Component[] components = t.GetComponents<Component>();

        foreach (Component comp in components)
        {
            if (comp == null)
                continue;

            sb.AppendLine($"{indent}  - {comp.GetType().FullName}");
        }

        sb.AppendLine();

        foreach (Transform child in t)
        {
            DumpTransform(child, sb, depth + 1);
        }
    }
}