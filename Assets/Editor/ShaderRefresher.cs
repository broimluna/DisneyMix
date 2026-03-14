using UnityEditor;
using UnityEngine;

public class ShaderRefresher {
    [MenuItem("Tools/Refresh UI Materials")]
    static void Refresh() {
        // This forces Unity to update every material's internal property block
        Shader.WarmupAllShaders();
        AssetDatabase.Refresh();
        Debug.Log("UI Materials Refreshed!");
    }
}