using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class SceneNamesScriptGenerator
{
    [MenuItem("Game/GenerateScript/SceneNames")]
    public static void GenerateSceneNamesClass()
    {
        StringBuilder classContent = new StringBuilder();
        classContent.AppendLine("//Script generated!");
        classContent.AppendLine("public static class SceneNames");
        classContent.AppendLine("{");

        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

        foreach (var scene in scenes)
        {
            if (scene.enabled)
            {
                string sceneName = Path.GetFileNameWithoutExtension(scene.path);
                classContent.AppendLine($"    public const string {sceneName} = \"{sceneName}\";");
            }
        }

        classContent.AppendLine("}");

        string filePath = Path.Combine("Assets/Content/Scripts/ScriptsGeneration/Scripts", "SceneNamesGenerated.cs");

        File.WriteAllText(filePath, classContent.ToString());
        AssetDatabase.Refresh();
    }
}
